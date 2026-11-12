using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Vendinha.Models;

namespace Vendinha.Data
{
    public class ClienteRepository
    {
        private readonly string _connectionString;

        public ClienteRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void InicializarBanco()
        {
            string masterConnection = "Server=(localdb)\\mssqllocaldb;Database=master;Trusted_Connection=True;";
            using (var connMaster = new SqlConnection(masterConnection))
            {
                connMaster.Open();
                string scriptDb = @"
                    IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'VendinhaDb')
                    BEGIN
                        CREATE DATABASE VendinhaDb;
                    END";
                using var cmd = new SqlCommand(scriptDb, connMaster);
                cmd.ExecuteNonQuery();
            }

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string scriptTabelas = @"
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Clientes')
                    BEGIN
                        CREATE TABLE Clientes (
                            Id INT IDENTITY(1,1) PRIMARY KEY,
                            NomeCompleto VARCHAR(150) NOT NULL,
                            Cpf VARCHAR(14) NOT NULL,
                            DataNascimento DATE NOT NULL,
                            Email VARCHAR(150) NOT NULL
                        );
                    END

                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Dividas')
                    BEGIN
                        CREATE TABLE Dividas (
                            Id INT IDENTITY(1,1) PRIMARY KEY,
                            ClienteId INT NOT NULL FOREIGN KEY REFERENCES Clientes(Id),
                            Valor DECIMAL(18,2) NOT NULL,
                            Paga BIT NOT NULL DEFAULT 0,
                            DataCriacao DATETIME NOT NULL,
                            DataPagamento DATETIME NULL
                        );
                    END";
                using var cmdTabelas = new SqlCommand(scriptTabelas, conn);
                cmdTabelas.ExecuteNonQuery();
            }

            PopularDadosIniciaisSeVazio();
        }

        private void PopularDadosIniciaisSeVazio()
        {
            if (ObterTotalClientes() > 0)
            {
                return;
            }

            var clientesIniciais = new List<(Cliente Cliente, decimal? DividaInicial)>
            {
                (new Cliente { NomeCompleto = "Ana Beatriz Souza", Cpf = "11122233344", DataNascimento = new DateTime(1995, 3, 15), Email = "ana.souza@email.com" }, 125.50m),
                (new Cliente { NomeCompleto = "Bruno Henrique Lima", Cpf = "22233344455", DataNascimento = new DateTime(1988, 7, 22), Email = "bruno.lima@email.com" }, 0m),
                (new Cliente { NomeCompleto = "Carla Mendes Oliveira", Cpf = "33344455566", DataNascimento = new DateTime(1992, 11, 5), Email = "carla.oliveira@email.com" }, 340.00m),
                (new Cliente { NomeCompleto = "Daniel Ferreira Costa", Cpf = "44455566677", DataNascimento = new DateTime(1980, 1, 30), Email = "daniel.costa@email.com" }, 89.90m),
                (new Cliente { NomeCompleto = "Eduarda Santos Rocha", Cpf = "55566677788", DataNascimento = new DateTime(2001, 6, 12), Email = "eduarda.rocha@email.com" }, 0m),
                (new Cliente { NomeCompleto = "Felipe Almeida Martins", Cpf = "66677788899", DataNascimento = new DateTime(1990, 9, 18), Email = "felipe.martins@email.com" }, 215.75m),
                (new Cliente { NomeCompleto = "Gabriela Nogueira Dias", Cpf = "77788899900", DataNascimento = new DateTime(1998, 4, 25), Email = "gabriela.dias@email.com" }, 54.00m),
                (new Cliente { NomeCompleto = "Henrique Barbosa Silva", Cpf = "88899900011", DataNascimento = new DateTime(1975, 12, 8), Email = "henrique.silva@email.com" }, 490.20m),
                (new Cliente { NomeCompleto = "Isabela Gomes Pereira", Cpf = "99900011122", DataNascimento = new DateTime(2003, 2, 14), Email = "isabela.pereira@email.com" }, 0m),
                (new Cliente { NomeCompleto = "João Pedro Carvalho", Cpf = "12312312301", DataNascimento = new DateTime(1985, 8, 19), Email = "joao.carvalho@email.com" }, 178.40m),
                (new Cliente { NomeCompleto = "Larissa Monteiro Ramos", Cpf = "23423423402", DataNascimento = new DateTime(1996, 5, 27), Email = "larissa.ramos@email.com" }, 62.30m),
                (new Cliente { NomeCompleto = "Marcos Vinícius Teixeira", Cpf = "34534534503", DataNascimento = new DateTime(1983, 10, 3), Email = "marcos.teixeira@email.com" }, 510.00m)
            };

            foreach (var item in clientesIniciais)
            {
                CadastrarClienteComDividaInicial(item.Cliente, item.DividaInicial);
            }
        }

        public void CadastrarClienteComDividaInicial(Cliente cliente, decimal? valorDividaInicial = null)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using var transaction = connection.BeginTransaction();

            try
            {
                string sqlCliente = @"INSERT INTO Clientes (NomeCompleto, Cpf, DataNascimento, Email) 
                                      OUTPUT INSERTED.Id 
                                      VALUES (@NomeCompleto, @Cpf, @DataNascimento, @Email);";

                using var cmdCliente = new SqlCommand(sqlCliente, connection, transaction);
                cmdCliente.Parameters.AddWithValue("@NomeCompleto", cliente.NomeCompleto);
                cmdCliente.Parameters.AddWithValue("@Cpf", cliente.Cpf);
                cmdCliente.Parameters.AddWithValue("@DataNascimento", cliente.DataNascimento);
                cmdCliente.Parameters.AddWithValue("@Email", cliente.Email);

                int clienteId = (int)cmdCliente.ExecuteScalar();

                if (valorDividaInicial.HasValue && valorDividaInicial.Value > 0)
                {
                    string sqlDivida = @"INSERT INTO Dividas (ClienteId, Valor, Paga, DataCriacao) 
                                         VALUES (@ClienteId, @Valor, 0, @DataCriacao);";

                    using var cmdDivida = new SqlCommand(sqlDivida, connection, transaction);
                    cmdDivida.Parameters.AddWithValue("@ClienteId", clienteId);
                    cmdDivida.Parameters.AddWithValue("@Valor", valorDividaInicial.Value);
                    cmdDivida.Parameters.AddWithValue("@DataCriacao", DateTime.Now);

                    cmdDivida.ExecuteNonQuery();
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public List<Cliente> ListarClientes()
        {
            var lista = new List<Cliente>();

            string sql = @"
                SELECT c.Id, c.NomeCompleto, c.Cpf, c.DataNascimento, c.Email, 
                       ISNULL(SUM(d.Valor), 0) AS TotalDevido
                FROM Clientes c
                LEFT JOIN Dividas d ON c.Id = d.ClienteId
                GROUP BY c.Id, c.NomeCompleto, c.Cpf, c.DataNascimento, c.Email";

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using var command = new SqlCommand(sql, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Cliente
                {
                    Id = reader.GetInt32(0),
                    NomeCompleto = reader.GetString(1),
                    Cpf = reader.GetString(2),
                    DataNascimento = reader.GetDateTime(3),
                    Email = reader.GetString(4),
                    TotalDevido = reader.GetDecimal(5)
                });
            }

            return lista;
        }

        public List<Cliente> ListarClientesPaginados(int pagina, int tamanhoPagina, string filtro = "")
        {
            var lista = new List<Cliente>();

            string sql = @"
                SELECT c.Id, c.NomeCompleto, c.Cpf, c.DataNascimento, c.Email, 
                       ISNULL(SUM(d.Valor), 0) AS TotalDevido
                FROM Clientes c
                LEFT JOIN Dividas d ON c.Id = d.ClienteId
                WHERE (@Filtro = '' OR c.NomeCompleto LIKE '%' + @Filtro + '%' OR c.Cpf LIKE '%' + @Filtro + '%')
                GROUP BY c.Id, c.NomeCompleto, c.Cpf, c.DataNascimento, c.Email
                ORDER BY c.NomeCompleto
                OFFSET @Offset ROWS FETCH NEXT @TamanhoPagina ROWS ONLY;";

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Filtro", string.IsNullOrWhiteSpace(filtro) ? "" : filtro);
            command.Parameters.AddWithValue("@Offset", (pagina - 1) * tamanhoPagina);
            command.Parameters.AddWithValue("@TamanhoPagina", tamanhoPagina);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Cliente
                {
                    Id = reader.GetInt32(0),
                    NomeCompleto = reader.GetString(1),
                    Cpf = reader.GetString(2),
                    DataNascimento = reader.GetDateTime(3),
                    Email = reader.GetString(4),
                    TotalDevido = reader.GetDecimal(5)
                });
            }

            return lista;
        }

        public int ObterTotalClientes(string filtro = "")
        {
            string sql = @"
                SELECT COUNT(DISTINCT c.Id)
                FROM Clientes c
                WHERE (@Filtro = '' OR c.NomeCompleto LIKE '%' + @Filtro + '%' OR c.Cpf LIKE '%' + @Filtro + '%');";

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Filtro", string.IsNullOrWhiteSpace(filtro) ? "" : filtro);

            return (int)command.ExecuteScalar();
        }

        public void ExcluirCliente(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using var transaction = connection.BeginTransaction();

            try
            {
                string sqlDividas = "DELETE FROM Dividas WHERE ClienteId = @ClienteId;";
                using var cmdDividas = new SqlCommand(sqlDividas, connection, transaction);
                cmdDividas.Parameters.AddWithValue("@ClienteId", id);
                cmdDividas.ExecuteNonQuery();

                string sqlCliente = "DELETE FROM Clientes WHERE Id = @Id;";
                using var cmdCliente = new SqlCommand(sqlCliente, connection, transaction);
                cmdCliente.Parameters.AddWithValue("@Id", id);
                cmdCliente.ExecuteNonQuery();

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void RegistrarPagamento(int clienteId, decimal valorPago)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            string sql = @"INSERT INTO Dividas (ClienteId, Valor, Paga, DataCriacao, DataPagamento) 
                          VALUES (@ClienteId, @Valor, 1, @DataCriacao, @DataPagamento);";

            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@ClienteId", clienteId);
            command.Parameters.AddWithValue("@Valor", -valorPago);
            command.Parameters.AddWithValue("@DataCriacao", DateTime.Now);
            command.Parameters.AddWithValue("@DataPagamento", DateTime.Now);
            command.ExecuteNonQuery();
        }

        public void AdicionarDivida(int clienteId, decimal valor)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            string sql = @"INSERT INTO Dividas (ClienteId, Valor, Paga, DataCriacao) 
                          VALUES (@ClienteId, @Valor, 0, @DataCriacao);";

            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@ClienteId", clienteId);
            command.Parameters.AddWithValue("@Valor", valor);
            command.Parameters.AddWithValue("@DataCriacao", DateTime.Now);

            command.ExecuteNonQuery();
        }
    }
}