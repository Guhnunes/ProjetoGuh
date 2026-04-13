using ProjetoGuh.Features.Menu;
using System;
using System.Windows.Forms;

// CERTIFIQUE-SE DE QUE ESTE NAMESPACE É O MESMO DO ARQUIVO .Designer.cs
namespace ProjetoGuh.Features.Menu
{
    public partial class MenuPrincipalForm : Form, IMenuPrincipalView
    {
        // Implementação dos eventos da Interface
        public event EventHandler AbrirClienteClick;
        public event EventHandler AbrirProdutoClick;
        public event EventHandler AbrirVendaClick;

        public MenuPrincipalForm()
        {
            // Agora o compilador vai encontrar este método no MenuPrincipalForm.Designer.cs
            InitializeComponent();

            // Configura os cliques dos menus
            menuItemCliente.Click += (s, e) => AbrirClienteClick?.Invoke(this, EventArgs.Empty);
            menuItemProduto.Click += (s, e) => AbrirProdutoClick?.Invoke(this, EventArgs.Empty);
            // Se tiver o menu de venda, adicione aqui também:
            // menuItemVenda.Click += (s, e) => AbrirVendaClick?.Invoke(this, EventArgs.Empty);
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
    }
}