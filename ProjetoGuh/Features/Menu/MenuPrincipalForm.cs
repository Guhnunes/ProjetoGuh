using ProjetoGuh.Features.Menu;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProjetoGuh.Features.Menu
{
    public partial class MenuPrincipalForm : Form, IMenuPrincipalView
    {
        public event EventHandler AbrirClienteClick;
        public event EventHandler AbrirProdutoClick;
        public event EventHandler AbrirVendaClick;

        public MenuPrincipalForm()
        {
            InitializeComponent();

            // Eventos de clique do Menu Superior
            menuItemCliente.Click += (s, e) => AbrirClienteClick?.Invoke(this, EventArgs.Empty);
            menuItemProduto.Click += (s, e) => AbrirProdutoClick?.Invoke(this, EventArgs.Empty);
            menuItemVenda.Click += (s, e) => AbrirVendaClick?.Invoke(this, EventArgs.Empty);

            // Configuração para o "X" nas abas
            tabControlPrincipal.DrawItem += TabControlPrincipal_DrawItem;
            tabControlPrincipal.MouseDown += TabControlPrincipal_MouseDown;
        }

        public void AdicionarAba(string titulo, Form formFilho)
        {
            foreach (TabPage tab in tabControlPrincipal.TabPages)
            {
                if (tab.Text == titulo)
                {
                    tabControlPrincipal.SelectedTab = tab;
                    return;
                }
            }

            TabPage novaAba = new TabPage(titulo);
            formFilho.TopLevel = false;
            formFilho.FormBorderStyle = FormBorderStyle.None;
            formFilho.Dock = DockStyle.Fill;

            novaAba.Controls.Add(formFilho);
            tabControlPrincipal.TabPages.Add(novaAba);

            formFilho.Show();
            tabControlPrincipal.SelectedTab = novaAba;
        }

        private void TabControlPrincipal_DrawItem(object sender, DrawItemEventArgs e)
        {
            var tabPage = tabControlPrincipal.TabPages[e.Index];
            var tabRect = tabControlPrincipal.GetTabRect(e.Index);

            var textRect = new Rectangle(tabRect.X + 5, tabRect.Y + 4, tabRect.Width - 25, tabRect.Height);
            TextRenderer.DrawText(e.Graphics, tabPage.Text, tabPage.Font, textRect, Color.Black, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);

            e.Graphics.DrawString("X", new Font("Arial", 8, FontStyle.Bold), Brushes.Red, tabRect.Right - 18, tabRect.Top + 6);
        }

        private void TabControlPrincipal_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < tabControlPrincipal.TabPages.Count; i++)
            {
                var tabRect = tabControlPrincipal.GetTabRect(i);
                var closeButtonRect = new Rectangle(tabRect.Right - 20, tabRect.Top, 20, tabRect.Height);

                if (closeButtonRect.Contains(e.Location))
                {
                    var page = tabControlPrincipal.TabPages[i];
                    tabControlPrincipal.TabPages.Remove(page);
                    page.Dispose();
                    break;
                }
            }
        }
    }
}