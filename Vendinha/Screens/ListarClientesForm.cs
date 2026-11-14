using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Vendinha.Models;
using Vendinha.Screens;
using Vendinha.Services;

namespace Vendinha
{
    public partial class ListarClientesForm : Form
    {
        private readonly ClienteService _service;
        private int paginaAtual = 1;
        private int tamanhoPagina = 10;
        private int totalPaginas = 1;

        public ListarClientesForm(ClienteService service)
        {
            InitializeComponent();
            _service = service;

            this.MinimumSize = new System.Drawing.Size(800, 520);
            if (this.ClientSize.Width < 800 || this.ClientSize.Height < 520)
            {
                this.ClientSize = new System.Drawing.Size(800, 520);
            }

            CarregarClientes();
        }

        private void CarregarClientes()
        {
            try
            {
                string filtro = txtPesquisa?.Text.Trim() ?? "";
                var resultado = _service.ObterClientesPaginados(paginaAtual, tamanhoPagina, filtro);

                totalPaginas = resultado.TotalPaginas;
                if (paginaAtual > totalPaginas) paginaAtual = totalPaginas;
                if (paginaAtual < 1) paginaAtual = 1;

                dgvClientes.DataSource = null;
                dgvClientes.DataSource = resultado.Clientes;
                AjustarColunasGrid();

                lblPaginacao.Text = $"Página {paginaAtual} de {totalPaginas}";
                btnAnterior.Enabled = paginaAtual > 1;
                btnProxima.Enabled = paginaAtual < totalPaginas;
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
                    dgvClientes.Columns["Id"].Visible = false;

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
            paginaAtual = 1;
            CarregarClientes();
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaAtual > 1)
            {
                paginaAtual--;
                CarregarClientes();
            }
        }

        private void btnProxima_Click(object sender, EventArgs e)
        {
            if (paginaAtual < totalPaginas)
            {
                paginaAtual++;
                CarregarClientes();
            }
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
                var confirmacao = MessageBox.Show($"Tem certeza que deseja mover o cliente {clienteSelecionado.NomeCompleto} para a lixeira?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmacao == DialogResult.Yes)
                {
                    try
                    {
                        _service.ExcluirCliente(clienteSelecionado.Id);
                        MessageBox.Show("Cliente enviado para a lixeira com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CarregarClientes();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao excluir cliente: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um cliente na lista para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvClientes.SelectedRows[0].DataBoundItem is Cliente clienteSelecionado)
            {
                var formEditar = new EditarClienteForm(_service, clienteSelecionado);
                formEditar.ShowDialog();
                CarregarClientes();
            }
        }

        private void btnDividas_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um cliente na lista para gerenciar as dívidas.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvClientes.SelectedRows[0].DataBoundItem is Cliente clienteSelecionado)
            {
                var formDividas = new HistoricoDividasForm(_service, clienteSelecionado);
                formDividas.ShowDialog();
                CarregarClientes();
            }
        }

        private void btnLixeira_Click(object sender, EventArgs e)
        {
            var formLixeira = new LixeiraForm(_service);
            formLixeira.ShowDialog();
            CarregarClientes();
        }

        private void ListarClientesForm_Load(object sender, EventArgs e)
        {
        }
    }
}