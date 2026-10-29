using System;
using System.Windows.Forms;
using Vendinha.Services;

namespace Vendinha
{
    public partial class CadastrarClienteForm : Form
    {
        private ClienteService _service;

        public CadastrarClienteForm(ClienteService service)
        {
            InitializeComponent();
            _service = service;
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Botão de cadastrar acionado com sucesso!", "Teste", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}