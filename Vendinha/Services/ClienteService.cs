using System;
using System.Collections.Generic;
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
            var cliente = new Cliente
            {
                NomeCompleto = nome,
                Cpf = cpf,
                DataNascimento = dataNascimento,
                Email = email
            };

            _repository.CadastrarClienteComDividaInicial(cliente, valorDividaInicial);
        }

        public List<Cliente> ObterTodos()
        {
            return _repository.ListarClientes();
        }

        public void ExcluirCliente(int id)
        {
            _repository.ExcluirCliente(id);
        }
    }
}