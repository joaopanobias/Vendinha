using System;
using System.Collections.Generic;
using System.Linq;
using Vendinha.Models;

namespace Vendinha.Services
{
    public class ClienteService
    {
        private readonly List<Cliente> _clientes = new();
        private int _proximoClienteId = 1;
        private int _proximoDividaId = 1;

        public void CadastrarCliente(string nome, string cpf, DateTime dataNascimento, string email)
        {
            if (_clientes.Any(c => c.Cpf == cpf))
            {
                throw new InvalidOperationException("Já existe um cliente cadastrado com este CPF.");
            }

            var cliente = new Cliente
            {
                Id = _proximoClienteId++,
                NomeCompleto = nome,
                Cpf = cpf,
                DataNascimento = dataNascimento,
                Email = email
            };

            _clientes.Add(cliente);
        }

        public void RegistrarDivida(int clienteId, decimal valor)
        {
            var cliente = _clientes.FirstOrDefault(c => c.Id == clienteId)
                ?? throw new ArgumentException("Cliente não encontrado.");

            if (cliente.DividaEmAberto)
            {
                throw new InvalidOperationException("O cliente já possui uma dívida em aberto. Quite-a antes de adicionar outra.");
            }

            var novaDivida = new Divida
            {
                Id = _proximoDividaId++,
                ClienteId = clienteId,
                Valor = valor
            };

            cliente.Dividas.Add(novaDivida);
        }

        public void PagarDivida(int clienteId, int dividaId)
        {
            var cliente = _clientes.FirstOrDefault(c => c.Id == clienteId)
                ?? throw new ArgumentException("Cliente não encontrado.");

            var divida = cliente.Dividas.FirstOrDefault(d => d.Id == dividaId)
                ?? throw new ArgumentException("Dívida não encontrada.");

            divida.ComoPaga();
        }

        public List<Cliente> ObterTodos() => _clientes;
    }
}