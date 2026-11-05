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
                       ISNULL(SUM(CASE WHEN d.Paga = 0 THEN d.Valor ELSE 0 END), 0) AS TotalDevido
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
    }
}