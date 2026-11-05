using System;
using System.Globalization;
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
                string nome = txtNome.Text.Trim();
                string cpf = txtCpf.Text.Trim();
                string email = txtEmail.Text.Trim();

                string textoData = txtDataNascimento.Text.Trim();
                string[] formatosAceitos = { "dd/MM/yyyy", "ddMMyyyy", "d/M/yyyy" };

                if (!DateTime.TryParseExact(textoData, formatosAceitos, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dataNascimento))
                {
                    MessageBox.Show("Data de nascimento inválida. Use o formato 12/04/2006 ou 12042006.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show($"Erro ao cadastrar cliente: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}