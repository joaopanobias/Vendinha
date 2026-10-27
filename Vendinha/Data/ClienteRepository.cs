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

            string sql = "SELECT Id, NomeCompleto, Cpf, DataNascimento, Email FROM Clientes";

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
                    Email = reader.GetString(4)
                });
            }

            return lista;
        }
    }
}