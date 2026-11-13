using System.Drawing;
using System.Windows.Forms;

namespace Vendinha
{
    partial class ListarClientesForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitulo = new Label();
            this.txtPesquisa = new TextBox();
            this.btnExcluir = new Button();
            this.btnEditar = new Button();
            this.btnDividas = new Button();
            this.dgvClientes = new DataGridView();
            this.btnAnterior = new Button();
            this.lblPaginacao = new Label();
            this.btnProxima = new Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).BeginInit();
            this.SuspendLayout();

            // lblTitulo
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            this.lblTitulo.ForeColor = Color.FromArgb(40, 40, 40);
            this.lblTitulo.Location = new Point(12, 18);
            this.lblTitulo.Text = "Lista de Clientes";

            // txtPesquisa
            this.txtPesquisa.Location = new Point(12, 70);
            this.txtPesquisa.Size = new Size(220, 23);
            this.txtPesquisa.TextChanged += new System.EventHandler(this.txtPesquisa_TextChanged);

            // btnExcluir
            this.btnExcluir.Location = new Point(240, 68);
            this.btnExcluir.Size = new Size(80, 25);
            this.btnExcluir.Text = "Excluir";
            this.btnExcluir.ForeColor = Color.Black;
            this.btnExcluir.UseVisualStyleBackColor = true;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);

            // btnEditar
            this.btnEditar.Location = new Point(326, 68);
            this.btnEditar.Size = new Size(80, 25);
            this.btnEditar.Text = "Editar";
            this.btnEditar.ForeColor = Color.Black;
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);

            // btnDividas
            this.btnDividas.Location = new Point(412, 68);
            this.btnDividas.Size = new Size(85, 25);
            this.btnDividas.Text = "Dívidas";
            this.btnDividas.ForeColor = Color.Black;
            this.btnDividas.UseVisualStyleBackColor = true;
            this.btnDividas.Click += new System.EventHandler(this.btnDividas_Click);

            // dgvClientes
            this.dgvClientes.Location = new Point(12, 105);
            this.dgvClientes.Size = new Size(760, 315);
            this.dgvClientes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvClientes.ReadOnly = true;
            this.dgvClientes.RowsDefaultCellStyle.ForeColor = Color.Black;
            this.dgvClientes.RowsDefaultCellStyle.BackColor = Color.White;

            // btnAnterior
            this.btnAnterior.Location = new Point(12, 432);
            this.btnAnterior.Size = new Size(85, 30);
            this.btnAnterior.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.btnAnterior.Text = "Anterior";
            this.btnAnterior.ForeColor = Color.Black;
            this.btnAnterior.UseVisualStyleBackColor = true;
            this.btnAnterior.Click += new System.EventHandler(this.btnAnterior_Click);

            // lblPaginacao
            this.lblPaginacao.Location = new Point(103, 438);
            this.lblPaginacao.Size = new Size(110, 20);
            this.lblPaginacao.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.lblPaginacao.TextAlign = ContentAlignment.MiddleCenter;
            this.lblPaginacao.Text = "Página 1 de 1";
            this.lblPaginacao.ForeColor = Color.Black;

            // btnProxima
            this.btnProxima.Location = new Point(219, 432);
            this.btnProxima.Size = new Size(85, 30);
            this.btnProxima.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.btnProxima.Text = "Próxima";
            this.btnProxima.ForeColor = Color.Black;
            this.btnProxima.UseVisualStyleBackColor = true;
            this.btnProxima.Click += new System.EventHandler(this.btnProxima_Click);

            // ListarClientesForm
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(800, 520);
            this.MinimumSize = new Size(800, 520);
            this.Name = "ListarClientesForm";
            this.Text = "Lista de Clientes";
            this.Load += new System.EventHandler(this.ListarClientesForm_Load);

            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.txtPesquisa);
            this.Controls.Add(this.btnExcluir);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnDividas);
            this.Controls.Add(this.dgvClientes);
            this.Controls.Add(this.btnAnterior);
            this.Controls.Add(this.lblPaginacao);
            this.Controls.Add(this.btnProxima);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).EndInit();
            this.ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitulo;
        private TextBox txtPesquisa;
        private Button btnExcluir;
        private Button btnEditar;
        private Button btnDividas;
        private DataGridView dgvClientes;
        private Button btnAnterior;
        private Label lblPaginacao;
        private Button btnProxima;
    }
}