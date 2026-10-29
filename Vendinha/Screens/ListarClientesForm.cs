using System;
using System.Windows.Forms;
using Vendinha.Services;

namespace Vendinha
{
    public partial class ListarClientesForm : Form
    {
        private ClienteService _service;

        // Construtor que recebe o serviço enviado pelo Form1
        public ListarClientesForm(ClienteService service)
        {
            InitializeComponent();
            _service = service;
        }
    }
}