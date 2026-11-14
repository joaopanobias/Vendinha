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
            ValidarDadosCliente(nome, ref cpf, dataNascimento, ref email, out string cpfLimpo);

            var clientesExistentes = _repository.ListarClientes();
            if (clientesExistentes.Any(c => new string(c.Cpf.Where(char.IsDigit).ToArray()) == cpfLimpo))
            {
                throw new Exception("Já existe um cliente cadastrado com este CPF.");
            }

            if (valorDividaInicial.HasValue && valorDividaInicial.Value < 0)
            {
                throw new Exception("O valor da dívida inicial não pode ser negativo.");
            }

            var cliente = new Cliente
            {
                NomeCompleto = nome.Trim(),
                Cpf = cpfLimpo,
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
            ValidarDadosCliente(nome, ref cpf, dataNascimento, ref email, out string cpfLimpo);

            var clientesExistentes = _repository.ListarClientes();
            if (clientesExistentes.Any(c => c.Id != id && new string(c.Cpf.Where(char.IsDigit).ToArray()) == cpfLimpo))
            {
                throw new Exception("Já existe outro cliente cadastrado com este CPF.");
            }

            var cliente = new Cliente
            {
                Id = id,
                NomeCompleto = nome.Trim(),
                Cpf = cpfLimpo,
                DataNascimento = dataNascimento,
                Email = email
            };

            _repository.AtualizarCliente(cliente);
        }

        private void ValidarDadosCliente(string nome, ref string cpf, DateTime dataNascimento, ref string email, out string cpfLimpo)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new Exception("O nome completo é obrigatório.");

            if (string.IsNullOrWhiteSpace(cpf))
                throw new Exception("O CPF é obrigatório.");

            ValidarIdade(dataNascimento);

            cpfLimpo = new string(cpf.Where(char.IsDigit).ToArray());

            if (!ValidarCpf(cpfLimpo))
                throw new Exception("O CPF informado é inválido.");

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
        }

        private void ValidarIdade(DateTime dataNascimento)
        {
            if (dataNascimento > DateTime.Now)
                throw new Exception("A data de nascimento não pode ser no futuro.");

            if (dataNascimento.Year < 1900)
                throw new Exception("Insira um ano de nascimento válido (com 4 dígitos, ex: 2000).");

            int idade = DateTime.Now.Year - dataNascimento.Year;
            if (dataNascimento.Date > DateTime.Now.AddYears(-idade))
                idade--;

            if (idade < 18 || idade > 120)
                throw new Exception("O cliente deve ser maior de idade entre 18 anos e 120 anos.");
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
                case "88888888888":
                case "99999999999":
                    return false;
            }

            int[] mult1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] mult2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * mult1[i];

            int resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            string digito = resto.ToString();
            tempCpf += digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * mult2[i];

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            digito += resto.ToString();

            return cpf.EndsWith(digito);
        }

        public List<Cliente> ObterTodos() => _repository.ListarClientes();

        public (List<Cliente> Clientes, int TotalPaginas) ObterClientesPaginados(int pagina, int tamanhoPagina, string filtro = "")
        {
            if (pagina < 1) pagina = 1;

            var clientes = _repository.ListarClientesPaginados(pagina, tamanhoPagina, filtro);
            int totalRegistros = _repository.ObterTotalClientes(filtro);

            int totalPaginas = (int)Math.Ceiling((double)totalRegistros / tamanhoPagina);
            if (totalPaginas == 0) totalPaginas = 1;

            return (clientes, totalPaginas);
        }

        public List<Cliente> ObterClientesNaLixeira() => _repository.ListarClientesExcluidos();

        public void RestaurarCliente(int id)
        {
            var excluidos = _repository.ListarClientesExcluidos();
            var clienteParaRestaurar = excluidos.FirstOrDefault(c => c.Id == id);

            if (clienteParaRestaurar == null)
                throw new Exception("Cliente não encontrado na lixeira.");

            var ativos = _repository.ListarClientes();
            string cpfLimpo = new string(clienteParaRestaurar.Cpf.Where(char.IsDigit).ToArray());

            if (ativos.Any(c => new string(c.Cpf.Where(char.IsDigit).ToArray()) == cpfLimpo))
                throw new Exception("Não é possível restaurar: já existe um cliente ativo cadastrado com este mesmo CPF.");

            _repository.RestaurarCliente(id);
        }

        public void ExcluirCliente(int id) => _repository.ExcluirCliente(id);

        public void RegistrarPagamento(int clienteId, decimal valorPago)
        {
            var cliente = _repository.ListarClientes().FirstOrDefault(c => c.Id == clienteId);
            if (cliente == null) throw new Exception("Cliente não encontrado.");
            if (cliente.TotalDevido <= 0) throw new Exception("Este cliente não possui dívidas pendentes.");
            if (valorPago <= 0) throw new Exception("O valor do pagamento deve ser maior que zero.");
            if (valorPago > cliente.TotalDevido) throw new Exception($"O valor do pagamento não pode ser superior ao total devido (R$ {cliente.TotalDevido:F2}).");

            _repository.RegistrarPagamento(clienteId, valorPago);
        }

        public void AdicionarDivida(int clienteId, decimal valor)
        {
            if (valor <= 0) throw new Exception("O valor da dívida deve ser maior que zero.");

            var cliente = _repository.ListarClientes().FirstOrDefault(c => c.Id == clienteId);
            if (cliente == null) throw new Exception("Cliente não encontrado.");
            if (cliente.TotalDevido > 0) throw new Exception("Este cliente já possui uma dívida em aberto. Ele deve quitar a dívida atual antes de contrair uma nova.");

            _repository.AdicionarDivida(clienteId, valor);
        }
    }
}