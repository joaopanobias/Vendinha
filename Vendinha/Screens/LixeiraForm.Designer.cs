using System.Drawing;
using System.Windows.Forms;

namespace Vendinha.Screens
{
    partial class LixeiraForm
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
            this.dataGridView1 = new DataGridView();
            this.btnRestaurar = new Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();

            // lblTitulo
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            this.lblTitulo.ForeColor = Color.FromArgb(40, 40, 40);
            this.lblTitulo.Location = new Point(12, 18);
            this.lblTitulo.Text = "Lixeira de Clientes";

            // dataGridView1
            this.dataGridView1.Location = new Point(12, 70);
            this.dataGridView1.Size = new Size(760, 335);
            this.dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.RowsDefaultCellStyle.ForeColor = Color.Black;
            this.dataGridView1.RowsDefaultCellStyle.BackColor = Color.White;

            // btnRestaurar
            this.btnRestaurar.Location = new Point(12, 418);
            this.btnRestaurar.Size = new Size(130, 35);
            this.btnRestaurar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.btnRestaurar.Text = "Restaurar Cliente";
            this.btnRestaurar.ForeColor = Color.Black;
            this.btnRestaurar.UseVisualStyleBackColor = true;
            this.btnRestaurar.Click += new System.EventHandler(this.btnRestaurar_Click);

            // LixeiraForm
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(800, 520);
            this.MinimumSize = new Size(800, 520);
            this.Name = "LixeiraForm";
            this.Text = "Lixeira de Clientes";

            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnRestaurar);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitulo;
        private DataGridView dataGridView1;
        private Button btnRestaurar;
    }
}