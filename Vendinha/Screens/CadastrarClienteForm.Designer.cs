using System.Drawing;
using System.Windows.Forms;

namespace Vendinha
{
    partial class CadastrarClienteForm
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
            this.lblNome = new Label();
            this.txtNome = new TextBox();
            this.lblCpf = new Label();
            this.txtCpf = new TextBox();
            this.lblNasc = new Label();
            this.txtDataNascimento = new TextBox();
            this.lblEmail = new Label();
            this.txtEmail = new TextBox();
            this.lblDivida = new Label();
            this.txtDividaInicial = new TextBox();
            this.btnSalvar = new Button();
            this.SuspendLayout();

            // lblTitulo
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblTitulo.ForeColor = Color.FromArgb(40, 40, 40);
            this.lblTitulo.Location = new Point(24, 20);
            this.lblTitulo.Text = "Novo Cliente";

            // lblNome & txtNome
            this.lblNome.AutoSize = true;
            this.lblNome.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblNome.Location = new Point(30, 75);
            this.lblNome.Text = "Nome Completo";

            this.txtNome.Location = new Point(30, 97);
            this.txtNome.Size = new Size(350, 25);

            // lblCpf & txtCpf
            this.lblCpf.AutoSize = true;
            this.lblCpf.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblCpf.Location = new Point(30, 135);
            this.lblCpf.Text = "CPF";

            this.txtCpf.Location = new Point(30, 157);
            this.txtCpf.Size = new Size(350, 25);

            // lblNasc & txtDataNascimento
            this.lblNasc.AutoSize = true;
            this.lblNasc.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblNasc.Location = new Point(30, 195);
            this.lblNasc.Text = "Data de Nascimento (dd/MM/yyyy)";

            this.txtDataNascimento.Location = new Point(30, 217);
            this.txtDataNascimento.Size = new Size(350, 25);

            // lblEmail & txtEmail
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblEmail.Location = new Point(30, 255);
            this.lblEmail.Text = "E-mail (Opcional)";

            this.txtEmail.Location = new Point(30, 277);
            this.txtEmail.Size = new Size(350, 25);

            // lblDivida & txtDividaInicial
            this.lblDivida.AutoSize = true;
            this.lblDivida.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblDivida.Location = new Point(30, 315);
            this.lblDivida.Text = "Dívida Inicial (Opcional)";

            this.txtDividaInicial.Location = new Point(30, 337);
            this.txtDividaInicial.Size = new Size(350, 25);

            // btnSalvar
            this.btnSalvar.Location = new Point(30, 390);
            this.btnSalvar.Size = new Size(150, 35);
            this.btnSalvar.Text = "Cadastrar Cliente";
            this.btnSalvar.ForeColor = Color.Black;
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);

            // CadastrarClienteForm
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(430, 455);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "CadastrarClienteForm";
            this.Text = "Cadastrar Cliente";

            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblNome);
            this.Controls.Add(this.txtNome);
            this.Controls.Add(this.lblCpf);
            this.Controls.Add(this.txtCpf);
            this.Controls.Add(this.lblNasc);
            this.Controls.Add(this.txtDataNascimento);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblDivida);
            this.Controls.Add(this.txtDividaInicial);
            this.Controls.Add(this.btnSalvar);
            this.ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitulo;
        private Label lblNome;
        private TextBox txtNome;
        private Label lblCpf;
        private TextBox txtCpf;
        private Label lblNasc;
        private TextBox txtDataNascimento;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblDivida;
        private TextBox txtDividaInicial;
        private Button btnSalvar;
    }
}