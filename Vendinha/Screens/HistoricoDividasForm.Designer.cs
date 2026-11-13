using System.Drawing;
using System.Windows.Forms;

namespace Vendinha
{
    partial class HistoricoDividasForm
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
            this.lblSubtitulo = new Label();
            this.dgvDividas = new DataGridView();
            this.btnNovaDivida = new Button();
            this.btnQuitar = new Button();
            this.btnFechar = new Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDividas)).BeginInit();
            this.SuspendLayout();

            // lblTitulo
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblTitulo.ForeColor = Color.FromArgb(40, 40, 40);
            this.lblTitulo.Location = new Point(20, 20);
            this.lblTitulo.Text = "Histórico de Dívidas";

            // lblSubtitulo
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            this.lblSubtitulo.ForeColor = Color.Black;
            this.lblSubtitulo.Location = new Point(20, 55);

            // btnNovaDivida
            this.btnNovaDivida.Location = new Point(20, 85);
            this.btnNovaDivida.Size = new Size(100, 30);
            this.btnNovaDivida.Text = "Nova Dívida";
            this.btnNovaDivida.ForeColor = Color.Black;
            this.btnNovaDivida.UseVisualStyleBackColor = true;
            this.btnNovaDivida.Click += new System.EventHandler(this.btnNovaDivida_Click);

            // btnQuitar
            this.btnQuitar.Location = new Point(126, 85);
            this.btnQuitar.Size = new Size(100, 30);
            this.btnQuitar.Text = "Quitar Dívida";
            this.btnQuitar.ForeColor = Color.Black;
            this.btnQuitar.UseVisualStyleBackColor = true;
            this.btnQuitar.Click += new System.EventHandler(this.btnQuitar_Click);

            // dgvDividas
            this.dgvDividas.Location = new Point(20, 125);
            this.dgvDividas.Size = new Size(645, 240);
            this.dgvDividas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.dgvDividas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDividas.ReadOnly = true;
            this.dgvDividas.RowsDefaultCellStyle.ForeColor = Color.Black;
            this.dgvDividas.RowsDefaultCellStyle.BackColor = Color.White;

            // btnFechar
            this.btnFechar.Location = new Point(565, 375);
            this.btnFechar.Size = new Size(100, 30);
            this.btnFechar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnFechar.Text = "Fechar";
            this.btnFechar.ForeColor = Color.Black;
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += (s, e) => this.Close();

            // HistoricoDividasForm
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(700, 450);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(700, 450);
            this.Name = "HistoricoDividasForm";
            this.Text = "Histórico de Dívidas";

            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblSubtitulo);
            this.Controls.Add(this.btnNovaDivida);
            this.Controls.Add(this.btnQuitar);
            this.Controls.Add(this.dgvDividas);
            this.Controls.Add(this.btnFechar);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDividas)).EndInit();
            this.ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitulo;
        private Label lblSubtitulo;
        private DataGridView dgvDividas;
        private Button btnNovaDivida;
        private Button btnQuitar;
        private Button btnFechar;
    }
}