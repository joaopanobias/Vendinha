using System;
using System.Globalization;
using System.Net.Mail;
using System.Windows.Forms;
using Vendinha.Models;
using Vendinha.Services;

namespace Vendinha
{
    public partial class EditarClienteForm : Form
    {
        private readonly ClienteService _service;
        private readonly Cliente _clienteParaEditar;

        public EditarClienteForm(ClienteService service, Cliente cliente)
        {
            InitializeComponent();
            _service = service;
            _clienteParaEditar = cliente;

            PreencherCampos();
        }

        private void PreencherCampos()
        {
            txtNome.Text = _clienteParaEditar.NomeCompleto;
            txtCpf.Text = _clienteParaEditar.Cpf;
            txtDataNascimento.Text = _clienteParaEditar.DataNascimento.ToString("dd/MM/yyyy");
            txtEmail.Text = _clienteParaEditar.Email;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNome.Text) || string.IsNullOrWhiteSpace(txtCpf.Text) ||
                    string.IsNullOrWhiteSpace(txtDataNascimento.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MessageBox.Show("Todos os campos são obrigatórios.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string nome = txtNome.Text.Trim();
                string cpf = txtCpf.Text.Trim();
                string email = txtEmail.Text.Trim();

                try
                {
                    var addr = new MailAddress(email);
                    if (addr.Address != email) throw new FormatException();
                }
                catch
                {
                    MessageBox.Show("E-mail inválido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string[] formatos = { "dd/MM/yyyy", "ddMMyyyy", "d/M/yyyy" };
                if (!DateTime.TryParseExact(txtDataNascimento.Text.Trim(), formatos, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dataNasc))
                {
                    MessageBox.Show("Data de nascimento inválida. Use o formato dd/MM/yyyy.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _service.AtualizarCliente(_clienteParaEditar.Id, nome, cpf, dataNasc, email);

                MessageBox.Show("Cliente atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}