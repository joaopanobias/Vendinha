using System;
using System.Windows.Forms;
using Vendinha.Services;

namespace Vendinha.Screens
{
    public partial class LixeiraForm : Form
    {
        private readonly ClienteService _service;

        public LixeiraForm(ClienteService service)
        {
            InitializeComponent();
            _service = service;
            CarregarLixeira();
        }

        private void CarregarLixeira()
        {
            try
            {
                var excluidos = _service.ObterClientesNaLixeira();
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = excluidos;
                AjustarColunasGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar lixeira: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AjustarColunasGrid()
        {
            try
            {
                if (dataGridView1.Columns["Id"] != null)
                    dataGridView1.Columns["Id"].Visible = false;

                if (dataGridView1.Columns["NomeCompleto"] != null)
                    dataGridView1.Columns["NomeCompleto"].HeaderText = "Nome Completo";

                if (dataGridView1.Columns["Cpf"] != null)
                    dataGridView1.Columns["Cpf"].HeaderText = "CPF";

                if (dataGridView1.Columns["DataNascimento"] != null)
                    dataGridView1.Columns["DataNascimento"].HeaderText = "Nascimento";

                if (dataGridView1.Columns["Email"] != null)
                    dataGridView1.Columns["Email"].HeaderText = "E-mail";

                if (dataGridView1.Columns["Idade"] != null)
                    dataGridView1.Columns["Idade"].HeaderText = "Idade";

                if (dataGridView1.Columns["TotalDevido"] != null)
                    dataGridView1.Columns["TotalDevido"].HeaderText = "Total Devido";

                if (dataGridView1.Columns["DividaEmAberto"] != null)
                    dataGridView1.Columns["DividaEmAberto"].HeaderText = "Dívida em Aberto";
            }
            catch
            {
            }
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um cliente para restaurar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int clienteId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);
                _service.RestaurarCliente(clienteId);

                MessageBox.Show("Cliente restaurado com sucesso junto com seu histórico!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CarregarLixeira();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}