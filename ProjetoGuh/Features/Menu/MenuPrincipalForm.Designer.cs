namespace ProjetoGuh.Features.Menu
{
    partial class MenuPrincipalForm
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

        #region Código gerado pelo Windows Form Designer

        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuCadastros = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemCliente = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemProduto = new System.Windows.Forms.ToolStripMenuItem();
            this.menuVendas = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemPdv = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemConsultaVendas = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlPrincipal = new System.Windows.Forms.TabControl();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuCadastros,
            this.menuVendas});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1350, 24);
            this.menuStrip1.TabIndex = 0;
            // 
            // menuCadastros
            // 
            this.menuCadastros.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemCliente,
            this.menuItemProduto});
            this.menuCadastros.Name = "menuCadastros";
            this.menuCadastros.Size = new System.Drawing.Size(71, 20);
            this.menuCadastros.Text = "&Cadastros";
            // 
            // menuItemCliente
            // 
            this.menuItemCliente.Name = "menuItemCliente";
            this.menuItemCliente.Size = new System.Drawing.Size(122, 22);
            this.menuItemCliente.Text = "Clientes";
            // 
            // menuItemProduto
            // 
            this.menuItemProduto.Name = "menuItemProduto";
            this.menuItemProduto.Size = new System.Drawing.Size(122, 22);
            this.menuItemProduto.Text = "Produtos";
            // 
            // menuVendas
            // 
            this.menuVendas.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemPdv,
            this.menuItemConsultaVendas});
            this.menuVendas.Name = "menuVendas";
            this.menuVendas.Size = new System.Drawing.Size(56, 20);
            this.menuVendas.Text = "&Vendas";
            // 
            // menuItemPdv
            // 
            this.menuItemPdv.Name = "menuItemPdv";
            this.menuItemPdv.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.menuItemPdv.Size = new System.Drawing.Size(206, 22);
            this.menuItemPdv.Text = "PDV (Frente de Caixa)";
            // 
            // menuItemConsultaVendas
            // 
            this.menuItemConsultaVendas.Name = "menuItemConsultaVendas";
            this.menuItemConsultaVendas.Size = new System.Drawing.Size(206, 22);
            this.menuItemConsultaVendas.Text = "Consultar Vendas";
            // 
            // tabControlPrincipal
            // 
            this.tabControlPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPrincipal.Location = new System.Drawing.Point(0, 24);
            this.tabControlPrincipal.Name = "tabControlPrincipal";
            this.tabControlPrincipal.SelectedIndex = 0;
            this.tabControlPrincipal.Size = new System.Drawing.Size(1350, 705);
            this.tabControlPrincipal.TabIndex = 1;
            // 
            // MenuPrincipalForm
            // 
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.tabControlPrincipal);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MenuPrincipalForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Projeto Guh - Sistema de Gestão";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuCadastros;
        private System.Windows.Forms.ToolStripMenuItem menuVendas;
        private System.Windows.Forms.ToolStripMenuItem menuItemCliente;
        private System.Windows.Forms.ToolStripMenuItem menuItemProduto;
        private System.Windows.Forms.ToolStripMenuItem menuItemPdv;
        private System.Windows.Forms.ToolStripMenuItem menuItemConsultaVendas; // Declaração do novo item
        private System.Windows.Forms.TabControl tabControlPrincipal;
    }
}