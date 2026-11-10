using System;
using System.Globalization;
using System.Net.Mail;
using System.Windows.Forms;
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

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNome.Text))
                {
                    MessageBox.Show("O campo Nome é obrigatório.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNome.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtCpf.Text))
                {
                    MessageBox.Show("O campo CPF é obrigatório.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCpf.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtDataNascimento.Text))
                {
                    MessageBox.Show("O campo Data de Nascimento é obrigatório.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDataNascimento.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MessageBox.Show("O campo E-mail é obrigatório.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return;
                }

                string nome = txtNome.Text.Trim();
                string cpf = txtCpf.Text.Trim();
                string email = txtEmail.Text.Trim();

                try
                {
                    var addr = new MailAddress(email);
                    if (addr.Address != email)
                    {
                        throw new FormatException();
                    }
                }
                catch
                {
                    MessageBox.Show("E-mail inválido. Insira um endereço de e-mail correto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return;
                }

                string textoData = txtDataNascimento.Text.Trim();
                string[] formatosAceitos = { "dd/MM/yyyy", "ddMMyyyy", "d/M/yyyy" };

                if (!DateTime.TryParseExact(textoData, formatosAceitos, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dataNascimento))
                {
                    MessageBox.Show("Data de nascimento inválida. Use o formato 12/04/2006 ou 12042006.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDataNascimento.Focus();
                    return;
                }

                int idade = DateTime.Today.Year - dataNascimento.Year;
                if (dataNascimento.Date > DateTime.Today.AddYears(-idade)) idade--;

                if (idade < 18 || idade > 120)
                {
                    MessageBox.Show("O cliente deve ter entre 18 e 120 anos para ser cadastrado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDataNascimento.Focus();
                    return;
                }

                decimal? valorDivida = null;
                if (!string.IsNullOrWhiteSpace(txtValorDivida.Text) && decimal.TryParse(txtValorDivida.Text, out decimal div))
                {
                    valorDivida = div;
                }

                _service.CadastrarClienteComDivida(nome, cpf, dataNascimento, email, valorDivida);

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