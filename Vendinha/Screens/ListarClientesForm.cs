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
        private Button btnQuitarDivida;
        private Button btnNovaDivida;
        private Button btnAnterior;
        private Button btnProxima;
        private Label lblPaginacao;

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

            AdicionarControlesDinamicos();
            CarregarClientes();
        }

        private void AdicionarControlesDinamicos()
        {
            if (txtPesquisa == null)
            {
                txtPesquisa = new TextBox { Name = "txtPesquisa", Location = new System.Drawing.Point(12, 70), Width = 220 };
                txtPesquisa.TextChanged += txtPesquisa_TextChanged;
                this.Controls.Add(txtPesquisa);
                txtPesquisa.BringToFront();
            }

            if (btnExcluir == null)
            {
                btnExcluir = new Button { Name = "btnExcluir", Text = "Excluir", Location = new System.Drawing.Point(240, 68), Width = 80, Height = 25, ForeColor = Color.Black };
                btnExcluir.Click += btnExcluir_Click;
                this.Controls.Add(btnExcluir);
                btnExcluir.BringToFront();
            }

            if (btnQuitarDivida == null)
            {
                btnQuitarDivida = new Button { Name = "btnQuitarDivida", Text = "Quitar Dívida", Location = new System.Drawing.Point(326, 68), Width = 95, Height = 25, ForeColor = Color.Black };
                btnQuitarDivida.Click += btnQuitarDivida_Click;
                this.Controls.Add(btnQuitarDivida);
                btnQuitarDivida.BringToFront();
            }

            if (btnNovaDivida == null)
            {
                btnNovaDivida = new Button { Name = "btnNovaDivida", Text = "Nova Dívida", Location = new System.Drawing.Point(427, 68), Width = 95, Height = 25, ForeColor = Color.Black };
                btnNovaDivida.Click += btnNovaDivida_Click;
                this.Controls.Add(btnNovaDivida);
                btnNovaDivida.BringToFront();
            }

            if (dgvClientes != null)
            {
                dgvClientes.Location = new System.Drawing.Point(12, 105);
                dgvClientes.Size = new System.Drawing.Size(760, 315);
                dgvClientes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                dgvClientes.RowsDefaultCellStyle.ForeColor = Color.Black;
                dgvClientes.RowsDefaultCellStyle.BackColor = Color.White;
            }

            if (btnAnterior == null)
            {
                btnAnterior = new Button { Name = "btnAnterior", Text = "Anterior", Location = new System.Drawing.Point(12, 432), Width = 85, Height = 30, Anchor = AnchorStyles.Bottom | AnchorStyles.Left, ForeColor = Color.Black, FlatStyle = FlatStyle.Standard };
                btnAnterior.Click += btnAnterior_Click;
                this.Controls.Add(btnAnterior);
                btnAnterior.BringToFront();
            }

            if (lblPaginacao == null)
            {
                lblPaginacao = new Label { Name = "lblPaginacao", Text = "Página 1 de 1", Location = new System.Drawing.Point(103, 438), Width = 110, Height = 20, TextAlign = System.Drawing.ContentAlignment.MiddleCenter, Anchor = AnchorStyles.Bottom | AnchorStyles.Left, ForeColor = Color.Black };
                this.Controls.Add(lblPaginacao);
                lblPaginacao.BringToFront();
            }

            if (btnProxima == null)
            {
                btnProxima = new Button { Name = "btnProxima", Text = "Próxima", Location = new System.Drawing.Point(219, 432), Width = 85, Height = 30, Anchor = AnchorStyles.Bottom | AnchorStyles.Left, ForeColor = Color.Black, FlatStyle = FlatStyle.Standard };
                btnProxima.Click += btnProxima_Click;
                this.Controls.Add(btnProxima);
                btnProxima.BringToFront();
            }
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
                var confirmacao = MessageBox.Show($"Tem certeza que deseja excluir o cliente {clienteSelecionado.NomeCompleto}?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmacao == DialogResult.Yes)
                {
                    try
                    {
                        _service.ExcluirCliente(clienteSelecionado.Id);
                        MessageBox.Show("Cliente excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CarregarClientes();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao excluir cliente: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnQuitarDivida_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um cliente na lista para registrar o pagamento.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvClientes.SelectedRows[0].DataBoundItem is Cliente clienteSelecionado)
            {
                if (clienteSelecionado.TotalDevido <= 0)
                {
                    MessageBox.Show("Este cliente não possui dívidas pendentes.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string input = Microsoft.VisualBasic.Interaction.InputBox(
                    $"Cliente: {clienteSelecionado.NomeCompleto}\nTotal Devido: R$ {clienteSelecionado.TotalDevido:F2}\n\nDigite o valor a ser pago:",
                    "Registrar Pagamento",
                    clienteSelecionado.TotalDevido.ToString("F2"));

                if (string.IsNullOrWhiteSpace(input))
                    return;

                if (decimal.TryParse(input, out decimal valorPago))
                {
                    try
                    {
                        _service.RegistrarPagamento(clienteSelecionado.Id, valorPago);
                        MessageBox.Show("Pagamento registrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CarregarClientes();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Valor inválido. Por favor, digite um número válido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnNovaDivida_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um cliente na lista para adicionar uma nova dívida.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvClientes.SelectedRows[0].DataBoundItem is Cliente clienteSelecionado)
            {
                string input = Microsoft.VisualBasic.Interaction.InputBox(
                    $"Digite o valor da nova dívida para {clienteSelecionado.NomeCompleto}:",
                    "Nova Dívida",
                    "0.00");

                if (string.IsNullOrWhiteSpace(input))
                    return;

                if (decimal.TryParse(input, out decimal valorDivida))
                {
                    try
                    {
                        _service.AdicionarDivida(clienteSelecionado.Id, valorDivida);
                        MessageBox.Show("Nova dívida adicionada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CarregarClientes();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Valor inválido. Por favor, digite um número válido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ListarClientesForm_Load(object sender, EventArgs e)
        {

        }

        private void ListarClientesForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}