using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Vendinha.Models;
using Vendinha.Services;

namespace Vendinha
{
    public partial class ListarClientesForm : Form
    {
        private readonly ClienteService _service;
        private TextBox txtPesquisa;
        private Button btnExcluir;

        public ListarClientesForm(ClienteService service)
        {
            InitializeComponent();
            _service = service;

            AdicionarControlesDinamicos();

            CarregarClientes();
        }

        private void AdicionarControlesDinamicos()
        {
            if (txtPesquisa == null)
            {
                txtPesquisa = new TextBox
                {
                    Name = "txtPesquisa",
                    Location = new System.Drawing.Point(12, 70),
                    Width = 250
                };
                txtPesquisa.TextChanged += txtPesquisa_TextChanged;
                this.Controls.Add(txtPesquisa);
                txtPesquisa.BringToFront();
            }

            if (btnExcluir == null)
            {
                btnExcluir = new Button
                {
                    Name = "btnExcluir",
                    Text = "Excluir",
                    Location = new System.Drawing.Point(270, 68),
                    Width = 90,
                    Height = 25
                };
                btnExcluir.Click += btnExcluir_Click;
                this.Controls.Add(btnExcluir);
                btnExcluir.BringToFront();
            }

            if (dgvClientes != null)
            {
                dgvClientes.Location = new System.Drawing.Point(12, 105);
                dgvClientes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            }
        }

        private void CarregarClientes(string filtro = "")
        {
            try
            {
                var clientes = _service.ObterTodos();

                if (!string.IsNullOrWhiteSpace(filtro))
                {
                    clientes = clientes.Where(c =>
                        c.NomeCompleto.Contains(filtro, StringComparison.OrdinalIgnoreCase) ||
                        c.Cpf.Contains(filtro)).ToList();
                }

                dgvClientes.DataSource = null;
                dgvClientes.DataSource = clientes;
                AjustarColunasGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar clientes: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AjustarColunasGrid()
        {
            try
            {
                if (dgvClientes.Columns["Id"] != null)
                    dgvClientes.Columns["Id"].HeaderText = "ID";

                if (dgvClientes.Columns["NomeCompleto"] != null)
                    dgvClientes.Columns["NomeCompleto"].HeaderText = "Nome Completo";

                if (dgvClientes.Columns["Cpf"] != null)
                    dgvClientes.Columns["Cpf"].HeaderText = "CPF";

                if (dgvClientes.Columns["DataNascimento"] != null)
                    dgvClientes.Columns["DataNascimento"].HeaderText = "Nascimento";

                if (dgvClientes.Columns["Email"] != null)
                    dgvClientes.Columns["Email"].HeaderText = "E-mail";

                if (dgvClientes.Columns["Idade"] != null)
                    dgvClientes.Columns["Idade"].HeaderText = "Idade";

                if (dgvClientes.Columns["TotalDevido"] != null)
                    dgvClientes.Columns["TotalDevido"].HeaderText = "Total Devido";

                if (dgvClientes.Columns["DividaEmAberto"] != null)
                    dgvClientes.Columns["DividaEmAberto"].HeaderText = "Dívida em Aberto";
            }
            catch
            {

            }
        }

        private void txtPesquisa_TextChanged(object sender, EventArgs e)
        {
            CarregarClientes(txtPesquisa.Text.Trim());
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um cliente na lista para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvClientes.SelectedRows[0].DataBoundItem is Cliente clienteSelecionado)
            {
                var confirmacao = MessageBox.Show($"Tem certeza que deseja excluir o cliente {clienteSelecionado.NomeCompleto}?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmacao == DialogResult.Yes)
                {
                    try
                    {
                        _service.ExcluirCliente(clienteSelecionado.Id);
                        MessageBox.Show("Cliente excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CarregarClientes(txtPesquisa?.Text.Trim() ?? "");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao excluir cliente: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}