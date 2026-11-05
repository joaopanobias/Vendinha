using System;
using System.Windows.Forms;
using Vendinha.Data;
using Vendinha.Services;

namespace Vendinha
{
    public partial class Form1 : Form
    {
        private readonly ClienteService _service;

        public Form1()
        {
            InitializeComponent();

            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=VendinhaDb;Trusted_Connection=True;";
            var repository = new ClienteRepository(connectionString);
            repository.InicializarBanco();
            _service = new ClienteService(repository);
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