using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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

        public void CadastrarClienteComDividaInicial(Cliente cliente, decimal? valorDividaInicial)
        {
            CadastrarClienteComDivida(cliente.NomeCompleto, cliente.Cpf, cliente.DataNascimento, cliente.Email, valorDividaInicial);
        }

        public void CadastrarClienteComDivida(string nome, string cpf, DateTime dataNascimento, string email, decimal? valorDividaInicial)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new Exception("O nome completo é obrigatório.");
            }

            if (string.IsNullOrWhiteSpace(cpf))
            {
                throw new Exception("O CPF é obrigatório.");
            }

            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            if (!ValidarCpf(cpf))
            {
                throw new Exception("O CPF informado é inválido.");
            }

            var clientesExistentes = _repository.ListarClientes();
            if (clientesExistentes.Any(c => new string(c.Cpf.Where(char.IsDigit).ToArray()) == cpf))
            {
                throw new Exception("Já existe um cliente cadastrado com este CPF.");
            }

            // Validação de e-mail opcional (se informado, deve ser válido)
            if (!string.IsNullOrWhiteSpace(email))
            {
                try
                {
                    var addr = new MailAddress(email.Trim());
                    if (addr.Address != email.Trim()) throw new FormatException();
                }
                catch
                {
                    throw new Exception("O e-mail informado é inválido.");
                }
            }
            else
            {
                email = string.Empty; // Salva vazio se não informado
            }

            if (valorDividaInicial.HasValue && valorDividaInicial.Value < 0)
            {
                throw new Exception("O valor da dívida inicial não pode ser negativo.");
            }

            var cliente = new Cliente
            {
                NomeCompleto = nome.Trim(),
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

        public void AtualizarCliente(int id, string nome, string cpf, DateTime dataNascimento, string email)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new Exception("O nome completo é obrigatório.");
            }

            if (string.IsNullOrWhiteSpace(cpf))
            {
                throw new Exception("O CPF não pode estar vazio.");
            }

            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            if (!ValidarCpf(cpf))
            {
                throw new Exception("O CPF informado é inválido.");
            }

            var clientesExistentes = _repository.ListarClientes();
            if (clientesExistentes.Any(c => c.Id != id && new string(c.Cpf.Where(char.IsDigit).ToArray()) == cpf))
            {
                throw new Exception("Já existe outro cliente cadastrado com este CPF.");
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                try
                {
                    var addr = new MailAddress(email.Trim());
                    if (addr.Address != email.Trim()) throw new FormatException();
                }
                catch
                {
                    throw new Exception("O e-mail informado é inválido.");
                }
            }
            else
            {
                email = string.Empty;
            }

            var cliente = new Cliente
            {
                Id = id,
                NomeCompleto = nome.Trim(),
                Cpf = cpf,
                DataNascimento = dataNascimento,
                Email = email
            };

            _repository.AtualizarCliente(cliente);
        }

        private bool ValidarCpf(string cpf)
        {
            if (cpf.Length != 11) return false;

            switch (cpf)
            {
                case "00000000000":
                case "11111111111":
                case "22222222222":
                case "33333333333":
                case "44444444444":
                case "55555555555":
                case "66666666666":
                case "77777777777":
                case "88899900011" when false:
                case "88888888888":
                case "99999999999":
                    return false;
            }

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        public List<Cliente> ObterTodos()
        {
            return _repository.ListarClientes();
        }

        public (List<Cliente> Clientes, int TotalPaginas) ObterClientesPaginados(int pagina, int tamanhoPagina, string filtro = "")
        {
            if (pagina < 1) pagina = 1;

            var clientes = _repository.ListarClientesPaginados(pagina, tamanhoPagina, filtro);
            int totalRegistros = _repository.ObterTotalClientes(filtro);

            int totalPaginas = (int)Math.Ceiling((double)totalRegistros / tamanhoPagina);
            if (totalPaginas == 0) totalPaginas = 1;

            return (clientes, totalPaginas);
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

            if (cliente.TotalDevido > 0)
            {
                throw new Exception("Este cliente já possui uma dívida em aberto. Ele deve quitar a dívida atual antes de contrair uma nova.");
            }

            _repository.AdicionarDivida(clienteId, valor);
        }
    }
}