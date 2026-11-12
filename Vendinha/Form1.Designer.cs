namespace Vendinha
{
    partial class Form1
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
            btnCadastrar = new Button();
            btnListar = new Button();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.ForeColor = Color.FromArgb(40, 40, 40);
            lblTitulo.Location = new Point(145, 37);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(159, 45);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Vendinha";
            // 
            // btnCadastrar
            // 
            btnCadastrar.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCadastrar.Location = new Point(96, 102);
            btnCadastrar.Name = "btnCadastrar";
            btnCadastrar.Size = new Size(250, 45);
            btnCadastrar.TabIndex = 1;
            btnCadastrar.Text = "Cadastrar Novo Cliente";
            btnCadastrar.UseVisualStyleBackColor = true;
            btnCadastrar.Click += btnCadastrar_Click;
            // 
            // btnListar
            // 
            btnListar.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnListar.Location = new Point(96, 161);
            btnListar.Name = "btnListar";
            btnListar.Size = new Size(250, 45);
            btnListar.TabIndex = 2;
            btnListar.Text = "Listar / Gerenciar Clientes";
            btnListar.UseVisualStyleBackColor = true;
            btnListar.Click += btnListar_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLight;
            ClientSize = new Size(450, 300);
            Controls.Add(btnListar);
            Controls.Add(btnCadastrar);
            Controls.Add(lblTitulo);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Vendinha - Menu Principal";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Button btnCadastrar;
        private Button btnListar;
    }
}