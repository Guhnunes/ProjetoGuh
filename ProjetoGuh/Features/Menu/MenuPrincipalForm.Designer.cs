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
            this.menuPrincipal = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemCliente = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemProduto = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemVenda = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlPrincipal = new System.Windows.Forms.TabControl();

            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();

            // MenuStrip
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.menuPrincipal });
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(900, 24);
            this.menuStrip1.TabIndex = 0;

            this.menuPrincipal.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.menuItemCliente,
                this.menuItemProduto,
                this.menuItemVenda
            });
            this.menuPrincipal.Text = "Menu";

            this.menuItemCliente.Text = "Clientes";
            this.menuItemProduto.Text = "Produtos";
            this.menuItemVenda.Text = "Vendas (PDV)";

            // TabControl (Ocupando tudo agora)
            this.tabControlPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPrincipal.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControlPrincipal.Padding = new System.Drawing.Point(22, 4);
            this.tabControlPrincipal.Location = new System.Drawing.Point(0, 24);
            this.tabControlPrincipal.Name = "tabControlPrincipal";

            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 650);
            this.Controls.Add(this.tabControlPrincipal);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MenuPrincipalForm";
            this.Text = "Projeto Guh - Sistema de Gestão";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuPrincipal;
        private System.Windows.Forms.ToolStripMenuItem menuItemCliente;
        private System.Windows.Forms.ToolStripMenuItem menuItemProduto;
        private System.Windows.Forms.ToolStripMenuItem menuItemVenda;
        private System.Windows.Forms.TabControl tabControlPrincipal;
    }
}