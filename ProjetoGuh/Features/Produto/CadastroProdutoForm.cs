using System;
using System.Windows.Forms;

namespace ProjetoGuh.Features.Produto
{
    // A classe deve ser partial para se unir ao Designer e implementar a View
    public partial class CadastroProdutoForm : Form, IProdutoView
    {
        public event EventHandler SalvarClick;

        public CadastroProdutoForm()
        {
            InitializeComponent();

            // Vincula o clique do botão ao evento que o Presenter escuta
            btnSalvar.Click += (s, e) => SalvarClick?.Invoke(this, EventArgs.Empty);
        }

        // Mapeamento dos campos da tela para a Interface
        public string Descricao
        {
            get => txtDescricao.Text;
            set => txtDescricao.Text = value;
        }

        public decimal Preco
        {
            get => decimal.TryParse(txtPreco.Text, out var p) ? p : 0;
            set => txtPreco.Text = value.ToString("N2");
        }

        public int Estoque
        {
            get => int.TryParse(txtEstoque.Text, out var e) ? e : 0;
            set => txtEstoque.Text = value.ToString();
        }

        public void MostrarMensagem(string msg)
        {
            MessageBox.Show(msg, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void LimparCampos()
        {
            txtDescricao.Clear();
            txtPreco.Clear();
            txtEstoque.Clear();
            txtDescricao.Focus();
        }
    }
}