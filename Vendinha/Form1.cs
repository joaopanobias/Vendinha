using System;
using System.Windows.Forms;
using Vendinha.Services;

namespace Vendinha
{
    public partial class Form1 : Form
    {
        private ClienteService _service;

        public Form1()
        {
            _service = new ClienteService();
            InitializeComponent();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            var telaCadastrar = new CadastrarClienteForm(_service);
            telaCadastrar.ShowDialog(this);
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            var telaListagem = new ListarClientesForm(_service);
            telaListagem.ShowDialog(this);
        }
    }
}