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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblTitulo = new Label();
            lblNome = new Label();
            txtNome = new TextBox();
            lblCpf = new Label();
            txtCpf = new TextBox();
            lblNasc = new Label();
            txtDataNascimento = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblDivida = new Label();
            txtValorDivida = new TextBox();
            btnSalvar = new Button();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitulo.Location = new Point(30, 20);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(230, 37);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Cadastrar Aluno";
            // 
            // lblNome
            // 
            lblNome.AutoSize = true;
            lblNome.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblNome.Location = new Point(30, 75);
            lblNome.Name = "lblNome";
            lblNome.Size = new Size(50, 19);
            lblNome.TabIndex = 1;
            lblNome.Text = "Nome";
            // 
            // txtNome
            // 
            txtNome.Location = new Point(30, 97);
            txtNome.Name = "txtNome";
            txtNome.Size = new Size(350, 23);
            txtNome.TabIndex = 2;
            // 
            // lblCpf
            // 
            lblCpf.AutoSize = true;
            lblCpf.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCpf.Location = new Point(30, 130);
            lblCpf.Name = "lblCpf";
            lblCpf.Size = new Size(33, 19);
            lblCpf.TabIndex = 3;
            lblCpf.Text = "CPF";
            // 
            // txtCpf
            // 
            txtCpf.Location = new Point(30, 152);
            txtCpf.Name = "txtCpf";
            txtCpf.Size = new Size(350, 23);
            txtCpf.TabIndex = 4;
            // 
            // lblNasc
            // 
            lblNasc.AutoSize = true;
            lblNasc.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblNasc.Location = new Point(30, 185);
            lblNasc.Name = "lblNasc";
            lblNasc.Size = new Size(144, 19);
            lblNasc.TabIndex = 5;
            lblNasc.Text = "Data de Nascimento";
            // 
            // txtDataNascimento
            // 
            txtDataNascimento.Location = new Point(30, 207);
            txtDataNascimento.Name = "txtDataNascimento";
            txtDataNascimento.Size = new Size(350, 23);
            txtDataNascimento.TabIndex = 6;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblEmail.Location = new Point(30, 240);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(45, 19);
            lblEmail.TabIndex = 7;
            lblEmail.Text = "Email";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(30, 262);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(350, 23);
            txtEmail.TabIndex = 8;
            // 
            // lblDivida
            // 
            lblDivida.AutoSize = true;
            lblDivida.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDivida.Location = new Point(30, 295);
            lblDivida.Name = "lblDivida";
            lblDivida.Size = new Size(157, 19);
            lblDivida.TabIndex = 9;
            lblDivida.Text = "Valor da Dívida Inicial";
            // 
            // txtValorDivida
            // 
            txtValorDivida.Location = new Point(30, 317);
            txtValorDivida.Name = "txtValorDivida";
            txtValorDivida.Size = new Size(350, 23);
            txtValorDivida.TabIndex = 10;
            // 
            // btnSalvar
            // 
            btnSalvar.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            btnSalvar.Location = new Point(30, 365);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(150, 40);
            btnSalvar.TabIndex = 11;
            btnSalvar.Text = "Cadastrar";
            btnSalvar.UseVisualStyleBackColor = true;
            btnSalvar.Click += BtnSalvar_Click;
            // 
            // CadastrarClienteForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(450, 435);
            Controls.Add(btnSalvar);
            Controls.Add(txtValorDivida);
            Controls.Add(lblDivida);
            Controls.Add(txtEmail);
            Controls.Add(lblEmail);
            Controls.Add(txtDataNascimento);
            Controls.Add(lblNasc);
            Controls.Add(txtCpf);
            Controls.Add(lblCpf);
            Controls.Add(txtNome);
            Controls.Add(lblNome);
            Controls.Add(lblTitulo);
            Name = "CadastrarClienteForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CadastrarAluno";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

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
        private TextBox txtValorDivida;
        private Button btnSalvar;
    }
}