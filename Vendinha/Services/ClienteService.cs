using System;
using System.Collections.Generic;
using System.Linq;
using Vendinha.Data;
using Vendinha.Models;

namespace Vendinha.Services
{
    public class ClienteService
    {
        private readonly ClienteRepository _repository;

        public ClienteService(ClienteRepository repository)
        {
            _repository = repository;
        }

        public void CadastrarClienteComDivida(string nome, string cpf, DateTime dataNascimento, string email, decimal? valorDividaInicial)
        {
            if (string.IsNullOrWhiteSpace(cpf))
            {
                throw new Exception("O CPF não pode estar vazio.");
            }

            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            if (cpf.Length != 11)
            {
                throw new Exception("O CPF deve conter exatamente 11 dígitos.");
            }

            var clientesExistentes = _repository.ListarClientes();
            if (clientesExistentes.Any(c => new string(c.Cpf.Where(char.IsDigit).ToArray()) == cpf))
            {
                throw new Exception("Já existe um cliente cadastrado com este CPF.");
            }

            if (valorDividaInicial.HasValue && valorDividaInicial.Value < 0)
            {
                throw new Exception("O valor da dívida inicial não pode ser negativo.");
            }

            var cliente = new Cliente
            {
                NomeCompleto = nome,
                Cpf = cpf,
                DataNascimento = dataNascimento,
                Email = email
            };

            try
            {
                _repository.CadastrarClienteComDividaInicial(cliente, valorDividaInicial);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao cadastrar cliente: {ex.Message}");
            }
        }

        public List<Cliente> ObterTodos()
        {
            return _repository.ListarClientes();
        }

        public void ExcluirCliente(int id)
        {
            _repository.ExcluirCliente(id);
        }

        public void RegistrarPagamento(int clienteId, decimal valorPago)
        {
            var cliente = _repository.ListarClientes().FirstOrDefault(c => c.Id == clienteId);
            if (cliente == null)
            {
                throw new Exception("Cliente não encontrado.");
            }

            if (cliente.TotalDevido <= 0)
            {
                throw new Exception("Este cliente não possui dívidas pendentes.");
            }

            if (valorPago <= 0)
            {
                throw new Exception("O valor do pagamento deve ser maior que zero.");
            }

            if (valorPago > cliente.TotalDevido)
            {
                throw new Exception($"O valor do pagamento não pode ser superior ao total devido (R$ {cliente.TotalDevido:F2}).");
            }

            _repository.RegistrarPagamento(clienteId, valorPago);
        }

        public void AdicionarDivida(int clienteId, decimal valor)
        {
            if (valor <= 0)
            {
                throw new Exception("O valor da dívida deve ser maior que zero.");
            }

            var cliente = _repository.ListarClientes().FirstOrDefault(c => c.Id == clienteId);
            if (cliente == null)
            {
                throw new Exception("Cliente não encontrado.");
            }

            _repository.AdicionarDivida(clienteId, valor);
        }
    }
}