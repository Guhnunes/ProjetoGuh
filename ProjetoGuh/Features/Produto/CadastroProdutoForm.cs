using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ProjetoGuh.Features.Produto
{
    public partial class CadastroProdutoForm : Form, ICadastroProdutoView
    {

        public event EventHandler BotaoSalvarFoiClicado;
        public event EventHandler BotaoCancelarFoiClicado;
        public event EventHandler BotaoExcluirFoiClicado;

        private readonly CadastroProdutoPresenter _presenter;
        private int _produtoIdAtual = 0;

        public CadastroProdutoForm(CadastroProdutoPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);

            btnSalvar.Click += (s, e) => BotaoSalvarFoiClicado?.Invoke(this, EventArgs.Empty);
            btnCancelar.Click += (s, e) => BotaoCancelarFoiClicado?.Invoke(this, EventArgs.Empty);
            btnExcluir.Click += (s, e) => BotaoExcluirFoiClicado?.Invoke(this, EventArgs.Empty);
            this.Load += (s, e) => _presenter.Inicializar();
            dataGridProdutoView1.CellClick += DataGridProdutoView1_CellClick;
        }
        public bool Ativo
        {
            get => chkAtivo.Checked;
            set => chkAtivo.Checked = value;
        }

        public ProdutoModel ObterDadosDoFormulario()
        {
            return new ProdutoModel
            {
                Id = _produtoIdAtual,
                Descricao = txtDescricao.Text,
                Preco = decimal.TryParse(txtPreco.Text, out var p) ? p : 0,
                Estoque = int.TryParse(txtEstoque.Text, out var e) ? e : 0,
                Ativo = Ativo ? 'S' : 'N'
            };
        }

        public void PreencherFormulario(ProdutoModel produto)
        {
            if (produto == null) return;

            _produtoIdAtual = produto.Id;
            txtDescricao.Text = produto.Descricao;
            txtPreco.Text = produto.Preco.ToString("N2");
            txtEstoque.Text = produto.Estoque.ToString();
            Ativo = produto.Ativo == 'S';
            chkAtivo.Enabled = true;
        }

        public void LimparFormulario()
        {
            _produtoIdAtual = 0;
            txtDescricao.Clear();
            txtPreco.Clear();
            txtEstoque.Clear();
            chkAtivo.Checked = true;
            chkAtivo.Enabled = false;
            txtDescricao.Focus();
        }

        public void PreencherGrid(List<ProdutoModel> produtos)
        {
            dataGridProdutoView1.DataSource = null;
            dataGridProdutoView1.DataSource = produtos;

            if (dataGridProdutoView1.Columns["Id"] != null)
                dataGridProdutoView1.Columns["Id"].Visible = false;
        }

        public ProdutoModel ObterProdutoSelecionado()
        {
            return dataGridProdutoView1.SelectedRows.Count > 0
                ? dataGridProdutoView1.SelectedRows[0].DataBoundItem as ProdutoModel
                : null;
        }

        private void DataGridProdutoView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var produto = ObterProdutoSelecionado();
            if (produto != null)
            {
                PreencherFormulario(produto);
            }
        }
    }
}