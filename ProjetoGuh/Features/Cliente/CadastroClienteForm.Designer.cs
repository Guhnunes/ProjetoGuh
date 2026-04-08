using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoGuh.Features.Cliente
{
    partial class CadastroClienteForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblNome = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.lblCpfCnpj = new System.Windows.Forms.Label();
            this.txtCpfCnpj = new System.Windows.Forms.TextBox();
            this.lblTelefone = new System.Windows.Forms.Label();
            this.txtTelefone = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblDataCadastro = new System.Windows.Forms.Label();
            this.dtpDataCadastro = new System.Windows.Forms.DateTimePicker();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();

            // lblNome
            this.lblNome.Text = "Nome:";
            this.lblNome.Location = new System.Drawing.Point(12, 15);
            this.lblNome.Size = new System.Drawing.Size(80, 20);

            // txtNome
            this.txtNome.Location = new System.Drawing.Point(100, 12);
            this.txtNome.Size = new System.Drawing.Size(300, 23);
            this.txtNome.MaxLength = 100;

            // lblCpfCnpj
            this.lblCpfCnpj.Text = "CPF/CNPJ:";
            this.lblCpfCnpj.Location = new System.Drawing.Point(12, 50);
            this.lblCpfCnpj.Size = new System.Drawing.Size(80, 20);

            // txtCpfCnpj
            this.txtCpfCnpj.Location = new System.Drawing.Point(100, 47);
            this.txtCpfCnpj.Size = new System.Drawing.Size(200, 23);
            this.txtCpfCnpj.MaxLength = 18;

            // lblTelefone
            this.lblTelefone.Text = "Telefone:";
            this.lblTelefone.Location = new System.Drawing.Point(12, 85);
            this.lblTelefone.Size = new System.Drawing.Size(80, 20);

            // txtTelefone
            this.txtTelefone.Location = new System.Drawing.Point(100, 82);
            this.txtTelefone.Size = new System.Drawing.Size(200, 23);
            this.txtTelefone.MaxLength = 20;

            // lblEmail
            this.lblEmail.Text = "Email:";
            this.lblEmail.Location = new System.Drawing.Point(12, 120);
            this.lblEmail.Size = new System.Drawing.Size(80, 20);

            // txtEmail
            this.txtEmail.Location = new System.Drawing.Point(100, 117);
            this.txtEmail.Size = new System.Drawing.Size(300, 23);
            this.txtEmail.MaxLength = 150;

            // lblDataCadastro
            this.lblDataCadastro.Text = "Data Cadastro:";
            this.lblDataCadastro.Location = new System.Drawing.Point(12, 155);
            this.lblDataCadastro.Size = new System.Drawing.Size(90, 20);

            // dtpDataCadastro
            this.dtpDataCadastro.Location = new System.Drawing.Point(100, 152);
            this.dtpDataCadastro.Size = new System.Drawing.Size(200, 23);
            this.dtpDataCadastro.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDataCadastro.Value = System.DateTime.Today;

            // btnSalvar
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.Location = new System.Drawing.Point(100, 195);
            this.btnSalvar.Size = new System.Drawing.Size(90, 30);
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);

            // btnCancelar
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Location = new System.Drawing.Point(200, 195);
            this.btnCancelar.Size = new System.Drawing.Size(90, 30);
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);

            // dataGridView1
            this.dataGridView1.Location = new System.Drawing.Point(12, 240);
            this.dataGridView1.Size = new System.Drawing.Size(760, 300);
            this.dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Top
                | System.Windows.Forms.AnchorStyles.Left
                | System.Windows.Forms.AnchorStyles.Right
                | System.Windows.Forms.AnchorStyles.Bottom;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            // CadastroClienteForm
            this.Text = "Cadastro de Clientes";
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.lblNome);
            this.Controls.Add(this.txtNome);
            this.Controls.Add(this.lblCpfCnpj);
            this.Controls.Add(this.txtCpfCnpj);
            this.Controls.Add(this.lblTelefone);
            this.Controls.Add(this.txtTelefone);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblDataCadastro);
            this.Controls.Add(this.dtpDataCadastro);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.dataGridView1);
            this.MinimumSize = new System.Drawing.Size(800, 600);

            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label lblCpfCnpj;
        private System.Windows.Forms.TextBox txtCpfCnpj;
        private System.Windows.Forms.Label lblTelefone;
        private System.Windows.Forms.TextBox txtTelefone;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblDataCadastro;
        private System.Windows.Forms.DateTimePicker dtpDataCadastro;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}