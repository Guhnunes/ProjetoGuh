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
            this.txtCpfCnpj = new System.Windows.Forms.MaskedTextBox();
            this.lblTelefone = new System.Windows.Forms.Label();
            this.txtTelefone = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblDataCadastro = new System.Windows.Forms.Label();
            this.dtpDataCadastro = new System.Windows.Forms.DateTimePicker();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            // 1. Instanciar o botão
            this.btnExcluir = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNome
            // 
            this.lblNome.Location = new System.Drawing.Point(12, 15);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(80, 20);
            this.lblNome.TabIndex = 0;
            this.lblNome.Text = "Nome:";
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(100, 12);
            this.txtNome.MaxLength = 100;
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(300, 22);
            this.txtNome.TabIndex = 1;
            // 
            // lblCpfCnpj
            // 
            this.lblCpfCnpj.Location = new System.Drawing.Point(12, 50);
            this.lblCpfCnpj.Name = "lblCpfCnpj";
            this.lblCpfCnpj.Size = new System.Drawing.Size(80, 20);
            this.lblCpfCnpj.TabIndex = 2;
            this.lblCpfCnpj.Text = "CPF/CNPJ:";
            // 
            // txtCpfCnpj
            // 
            this.txtCpfCnpj.Location = new System.Drawing.Point(100, 48);
            this.txtCpfCnpj.Mask = "000.000.000-00";
            this.txtCpfCnpj.Name = "txtCpfCnpj";
            this.txtCpfCnpj.Size = new System.Drawing.Size(200, 22);
            this.txtCpfCnpj.TabIndex = 3;
            // 
            // lblTelefone
            // 
            this.lblTelefone.Location = new System.Drawing.Point(12, 85);
            this.lblTelefone.Name = "lblTelefone";
            this.lblTelefone.Size = new System.Drawing.Size(80, 20);
            this.lblTelefone.TabIndex = 4;
            this.lblTelefone.Text = "Telefone:";
            // 
            // txtTelefone
            // 
            this.txtTelefone.Location = new System.Drawing.Point(100, 82);
            this.txtTelefone.MaxLength = 20;
            this.txtTelefone.Name = "txtTelefone";
            this.txtTelefone.Size = new System.Drawing.Size(200, 22);
            this.txtTelefone.TabIndex = 5;
            // 
            // lblEmail
            // 
            this.lblEmail.Location = new System.Drawing.Point(12, 120);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(80, 20);
            this.lblEmail.TabIndex = 6;
            this.lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(100, 117);
            this.txtEmail.MaxLength = 150;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(300, 22);
            this.txtEmail.TabIndex = 7;
            // 
            // lblDataCadastro
            // 
            this.lblDataCadastro.Location = new System.Drawing.Point(12, 155);
            this.lblDataCadastro.Name = "lblDataCadastro";
            this.lblDataCadastro.Size = new System.Drawing.Size(90, 20);
            this.lblDataCadastro.TabIndex = 8;
            this.lblDataCadastro.Text = "Data Cadastro:";
            // 
            // dtpDataCadastro
            // 
            this.dtpDataCadastro.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDataCadastro.Location = new System.Drawing.Point(100, 152);
            this.dtpDataCadastro.Name = "dtpDataCadastro";
            this.dtpDataCadastro.Size = new System.Drawing.Size(200, 22);
            this.dtpDataCadastro.TabIndex = 9;
            this.dtpDataCadastro.Value = new System.DateTime(2026, 4, 8, 0, 0, 0, 0);
            // 
            // btnSalvar
            // 
            this.btnSalvar.Location = new System.Drawing.Point(100, 195);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(90, 30);
            this.btnSalvar.TabIndex = 10;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(200, 195);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(90, 30);
            this.btnCancelar.TabIndex = 11;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnExcluir
            // 
            // 2. Configurar Propriedades do Botão Excluir
            this.btnExcluir.Location = new System.Drawing.Point(300, 195);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(90, 30);
            this.btnExcluir.TabIndex = 13;
            this.btnExcluir.Text = "Excluir";
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeight = 29;
            this.dataGridView1.Location = new System.Drawing.Point(12, 240);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(760, 300);
            this.dataGridView1.TabIndex = 12;
            // 
            // CadastroClienteForm
            // 
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.txtCpfCnpj);
            this.Controls.Add(this.lblNome);
            this.Controls.Add(this.txtNome);
            this.Controls.Add(this.lblCpfCnpj);
            this.Controls.Add(this.lblTelefone);
            this.Controls.Add(this.txtTelefone);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblDataCadastro);
            this.Controls.Add(this.dtpDataCadastro);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.btnCancelar);
            // 3. Adicionar o controle ao formulário
            this.Controls.Add(this.btnExcluir);
            this.Controls.Add(this.dataGridView1);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "CadastroClienteForm";
            this.Text = "Cadastro de Clientes";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label lblCpfCnpj;
        private System.Windows.Forms.Label lblTelefone;
        private System.Windows.Forms.TextBox txtTelefone;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblDataCadastro;
        private System.Windows.Forms.DateTimePicker dtpDataCadastro;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnCancelar;
        // 4. Declarar o botão
        private System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.MaskedTextBox txtCpfCnpj;
    }
}