using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Vendinha.Models;
using Vendinha.Services;

namespace Vendinha
{
    public partial class HistoricoDividasForm : Form
    {
        private readonly ClienteService _service;
        private readonly Cliente _cliente;

        public HistoricoDividasForm(ClienteService service, Cliente cliente)
        {
            InitializeComponent();
            _service = service;
            _cliente = cliente;

            CarregarDividas();
        }

        private void CarregarDividas()
        {
            try
            {
                var clienteAtualizado = _service.ObterTodos().FirstOrDefault(c => c.Id == _cliente.Id);
                if (clienteAtualizado != null)
                {
                    lblSubtitulo.Text = $"Cliente: {clienteAtualizado.NomeCompleto} | Total Devido: R$ {clienteAtualizado.TotalDevido:F2}";

                    var listaExibicao = clienteAtualizado.Dividas.Select(d => new
                    {
                        d.Id,
                        ValorFormatado = $"R$ {d.Valor:F2}",
                        Situacao = d.Paga ? "Paga" : "Em Aberto",
                        DataCriacao = d.DataCriacao.ToString("dd/MM/yyyy HH:mm"),
                        DataPagamento = d.DataPagamento.HasValue ? d.DataPagamento.Value.ToString("dd/MM/yyyy HH:mm") : "-"
                    }).ToList();

                    dgvDividas.DataSource = null;
                    dgvDividas.DataSource = listaExibicao;

                    if (dgvDividas.Columns["Id"] != null)
                        dgvDividas.Columns["Id"].Visible = false;

                    if (dgvDividas.Columns["ValorFormatado"] != null)
                        dgvDividas.Columns["ValorFormatado"].HeaderText = "Valor";

                    if (dgvDividas.Columns["Situacao"] != null)
                        dgvDividas.Columns["Situacao"].HeaderText = "Situação";

                    if (dgvDividas.Columns["DataCriacao"] != null)
                        dgvDividas.Columns["DataCriacao"].HeaderText = "Data de Criação";

                    if (dgvDividas.Columns["DataPagamento"] != null)
                        dgvDividas.Columns["DataPagamento"].HeaderText = "Data de Pagamento";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar dívidas: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNovaDivida_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox(
                $"Digite o valor da nova dívida para {_cliente.NomeCompleto}:",
                "Nova Dívida",
                "0.00");

            if (string.IsNullOrWhiteSpace(input))
                return;

            if (decimal.TryParse(input, out decimal valor))
            {
                try
                {
                    _service.AdicionarDivida(_cliente.Id, valor);
                    MessageBox.Show("Nova dívida adicionada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CarregarDividas();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Valor inválido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            try
            {
                var clienteAtualizado = _service.ObterTodos().FirstOrDefault(c => c.Id == _cliente.Id);
                if (clienteAtualizado == null || clienteAtualizado.TotalDevido <= 0)
                {
                    MessageBox.Show("Este cliente não possui dívidas pendentes.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string input = Microsoft.VisualBasic.Interaction.InputBox(
                    $"Total Devido: R$ {clienteAtualizado.TotalDevido:F2}\n\nDigite o valor a ser pago:",
                    "Quitar Dívida",
                    clienteAtualizado.TotalDevido.ToString("F2"));

                if (string.IsNullOrWhiteSpace(input))
                    return;

                if (decimal.TryParse(input, out decimal valorPago))
                {
                    _service.RegistrarPagamento(_cliente.Id, valorPago);
                    MessageBox.Show("Pagamento registrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CarregarDividas();
                }
                else
                {
                    MessageBox.Show("Valor inválido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}