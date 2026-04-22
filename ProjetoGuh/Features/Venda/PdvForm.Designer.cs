namespace ProjetoGuh.Features.Venda
{
    partial class PdvForm
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
            this.grpCabecalho = new System.Windows.Forms.GroupBox();
            this.lblCliente = new System.Windows.Forms.Label();
            this.cmbCliente = new System.Windows.Forms.ComboBox();
            this.lblFormaPagto = new System.Windows.Forms.Label();
            this.cmbFormaPagamento = new System.Windows.Forms.ComboBox();
            this.grpItens = new System.Windows.Forms.GroupBox();
            this.cmbProduto = new System.Windows.Forms.ComboBox();
            this.txtQuantidade = new System.Windows.Forms.NumericUpDown();
            this.txtPrecoUnitario = new System.Windows.Forms.TextBox();
            this.btnAdicionar = new System.Windows.Forms.Button();
            this.btnRemover = new System.Windows.Forms.Button();
            this.dgvItens = new System.Windows.Forms.DataGridView();
            this.lblTotalVenda = new System.Windows.Forms.Label();
            this.btnFinalizar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.txtObservacao = new System.Windows.Forms.TextBox();
            this.lblObs = new System.Windows.Forms.Label();
            this.grpCabecalho.SuspendLayout();
            this.grpItens.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtQuantidade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItens)).BeginInit();
            this.SuspendLayout();
            // 
            // grpCabecalho
            // 
            this.grpCabecalho.Controls.Add(this.lblCliente);
            this.grpCabecalho.Controls.Add(this.cmbCliente);
            this.grpCabecalho.Controls.Add(this.lblFormaPagto);
            this.grpCabecalho.Controls.Add(this.cmbFormaPagamento);
            this.grpCabecalho.Location = new System.Drawing.Point(12, 12);
            this.grpCabecalho.Name = "grpCabecalho";
            this.grpCabecalho.Size = new System.Drawing.Size(1309, 80);
            this.grpCabecalho.TabIndex = 0;
            this.grpCabecalho.TabStop = false;
            this.grpCabecalho.Text = "Dados da Venda";
            // 
            // lblCliente
            // 
            this.lblCliente.Location = new System.Drawing.Point(10, 18);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(100, 21);
            this.lblCliente.TabIndex = 0;
            this.lblCliente.Text = "Cliente:";
            // 
            // cmbCliente
            // 
            this.cmbCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCliente.Location = new System.Drawing.Point(10, 42);
            this.cmbCliente.Name = "cmbCliente";
            this.cmbCliente.Size = new System.Drawing.Size(594, 24);
            this.cmbCliente.TabIndex = 1;
            // 
            // lblFormaPagto
            // 
            this.lblFormaPagto.Location = new System.Drawing.Point(626, 18);
            this.lblFormaPagto.Name = "lblFormaPagto";
            this.lblFormaPagto.Size = new System.Drawing.Size(150, 21);
            this.lblFormaPagto.TabIndex = 2;
            this.lblFormaPagto.Text = "Forma de Pagamento:";
            // 
            // cmbFormaPagamento
            // 
            this.cmbFormaPagamento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormaPagamento.Location = new System.Drawing.Point(625, 42);
            this.cmbFormaPagamento.Name = "cmbFormaPagamento";
            this.cmbFormaPagamento.Size = new System.Drawing.Size(200, 24);
            this.cmbFormaPagamento.TabIndex = 3;
            // 
            // grpItens
            // 
            this.grpItens.Controls.Add(this.cmbProduto);
            this.grpItens.Controls.Add(this.txtQuantidade);
            this.grpItens.Controls.Add(this.txtPrecoUnitario);
            this.grpItens.Controls.Add(this.btnAdicionar);
            this.grpItens.Controls.Add(this.btnRemover);
            this.grpItens.Controls.Add(this.dgvItens);
            this.grpItens.Location = new System.Drawing.Point(12, 100);
            this.grpItens.Name = "grpItens";
            this.grpItens.Size = new System.Drawing.Size(1309, 414);
            this.grpItens.TabIndex = 1;
            this.grpItens.TabStop = false;
            this.grpItens.Text = "Lançamento de Itens";
            // 
            // cmbProduto
            // 
            this.cmbProduto.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbProduto.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbProduto.Location = new System.Drawing.Point(10, 31);
            this.cmbProduto.Name = "cmbProduto";
            this.cmbProduto.Size = new System.Drawing.Size(594, 24);
            this.cmbProduto.TabIndex = 0;
            // 
            // txtQuantidade
            // 
            this.txtQuantidade.Location = new System.Drawing.Point(610, 32);
            this.txtQuantidade.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtQuantidade.Name = "txtQuantidade";
            this.txtQuantidade.Size = new System.Drawing.Size(60, 22);
            this.txtQuantidade.TabIndex = 1;
            this.txtQuantidade.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txtPrecoUnitario
            // 
            this.txtPrecoUnitario.Location = new System.Drawing.Point(676, 31);
            this.txtPrecoUnitario.Name = "txtPrecoUnitario";
            this.txtPrecoUnitario.ReadOnly = true;
            this.txtPrecoUnitario.Size = new System.Drawing.Size(100, 22);
            this.txtPrecoUnitario.TabIndex = 2;
            this.txtPrecoUnitario.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Location = new System.Drawing.Point(1141, 30);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(78, 25);
            this.btnAdicionar.TabIndex = 3;
            this.btnAdicionar.Text = "Adicionar";
            // 
            // btnRemover
            // 
            this.btnRemover.Location = new System.Drawing.Point(1225, 30);
            this.btnRemover.Name = "btnRemover";
            this.btnRemover.Size = new System.Drawing.Size(78, 25);
            this.btnRemover.TabIndex = 4;
            this.btnRemover.Text = "Remover";
            // 
            // dgvItens
            // 
            this.dgvItens.AllowUserToAddRows = false;
            this.dgvItens.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvItens.ColumnHeadersHeight = 29;
            this.dgvItens.Location = new System.Drawing.Point(10, 61);
            this.dgvItens.MultiSelect = false;
            this.dgvItens.Name = "dgvItens";
            this.dgvItens.ReadOnly = true;
            this.dgvItens.RowHeadersWidth = 51;
            this.dgvItens.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItens.Size = new System.Drawing.Size(1293, 347);
            this.dgvItens.TabIndex = 5;
            // 
            // lblTotalVenda
            // 
            this.lblTotalVenda.Font = new System.Drawing.Font("Segoe UI", 35F, System.Drawing.FontStyle.Bold);
            this.lblTotalVenda.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblTotalVenda.Location = new System.Drawing.Point(1082, 517);
            this.lblTotalVenda.Name = "lblTotalVenda";
            this.lblTotalVenda.Size = new System.Drawing.Size(200, 69);
            this.lblTotalVenda.TabIndex = 2;
            this.lblTotalVenda.Text = "R$ 0,00";
            this.lblTotalVenda.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.BackColor = System.Drawing.Color.LightGreen;
            this.btnFinalizar.Location = new System.Drawing.Point(1117, 593);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(150, 40);
            this.btnFinalizar.TabIndex = 3;
            this.btnFinalizar.Text = "Finalizar Venda (F5)";
            this.btnFinalizar.UseVisualStyleBackColor = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(961, 593);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(150, 40);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.Text = "Cancelar";
            // 
            // txtObservacao
            // 
            this.txtObservacao.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtObservacao.Location = new System.Drawing.Point(22, 560);
            this.txtObservacao.Multiline = true;
            this.txtObservacao.Name = "txtObservacao";
            this.txtObservacao.Size = new System.Drawing.Size(766, 73);
            this.txtObservacao.TabIndex = 6;
            // 
            // lblObs
            // 
            this.lblObs.Location = new System.Drawing.Point(19, 542);
            this.lblObs.Name = "lblObs";
            this.lblObs.Size = new System.Drawing.Size(100, 15);
            this.lblObs.TabIndex = 5;
            this.lblObs.Text = "Observação:";
            // 
            // PdvForm
            // 
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.lblObs);
            this.Controls.Add(this.txtObservacao);
            this.Controls.Add(this.grpCabecalho);
            this.Controls.Add(this.grpItens);
            this.Controls.Add(this.lblTotalVenda);
            this.Controls.Add(this.btnFinalizar);
            this.Controls.Add(this.btnCancelar);
            this.Name = "PdvForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ProjetoGuh - PDV";
            this.grpCabecalho.ResumeLayout(false);
            this.grpItens.ResumeLayout(false);
            this.grpItens.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtQuantidade)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItens)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.GroupBox grpCabecalho;
        private System.Windows.Forms.ComboBox cmbCliente;
        private System.Windows.Forms.ComboBox cmbFormaPagamento;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Label lblFormaPagto;
        private System.Windows.Forms.GroupBox grpItens;
        private System.Windows.Forms.ComboBox cmbProduto;
        private System.Windows.Forms.NumericUpDown txtQuantidade;
        private System.Windows.Forms.TextBox txtPrecoUnitario;
        private System.Windows.Forms.Button btnAdicionar;
        private System.Windows.Forms.Button btnRemover;
        private System.Windows.Forms.DataGridView dgvItens;
        private System.Windows.Forms.Label lblTotalVenda;
        private System.Windows.Forms.Button btnFinalizar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.TextBox txtObservacao;
        private System.Windows.Forms.Label lblObs;
    }
}