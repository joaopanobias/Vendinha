using System;
using System.Globalization;
using System.Windows.Forms;
using Vendinha.Models;
using Vendinha.Services;

namespace Vendinha
{
    public partial class CadastrarClienteForm : Form
    {
        private readonly ClienteService _service;

        public CadastrarClienteForm(ClienteService service)
        {
            InitializeComponent();
            _service = service;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNome.Text) ||
                    string.IsNullOrWhiteSpace(txtCpf.Text) ||
                    string.IsNullOrWhiteSpace(txtDataNascimento.Text))
                {
                    MessageBox.Show("Preencha os campos obrigatórios: Nome Completo, CPF e Data de Nascimento.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string nome = txtNome.Text.Trim();
                string cpf = txtCpf.Text.Trim();
                string email = txtEmail.Text.Trim();

                string[] formatos = { "dd/MM/yyyy", "ddMMyyyy", "d/M/yyyy" };
                if (!DateTime.TryParseExact(txtDataNascimento.Text.Trim(), formatos, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dataNasc))
                {
                    MessageBox.Show("Data de nascimento inválida. Use o formato com 4 dígitos no ano (ex: dd/MM/yyyy).", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                decimal? dividaInicial = null;
                if (!string.IsNullOrWhiteSpace(txtDividaInicial?.Text))
                {
                    if (decimal.TryParse(txtDividaInicial.Text.Trim(), out decimal valDiv))
                    {
                        dividaInicial = valDiv;
                    }
                }

                var novoCliente = new Cliente
                {
                    NomeCompleto = nome,
                    Cpf = cpf,
                    DataNascimento = dataNasc,
                    Email = email
                };

                _service.CadastrarClienteComDividaInicial(novoCliente, dividaInicial);

                MessageBox.Show("Cliente cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}