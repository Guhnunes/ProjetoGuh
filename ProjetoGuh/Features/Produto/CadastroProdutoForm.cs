using ProjetoGuh.Features.Infraestrutura;
using ProjetoGuh.Features.Produto.Model;
using ProjetoGuh.Features.Produto.Presenter;
using ProjetoGuh.Features.Produto.View;
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
            // Limpa a string da máscara para converter para decimal puro
            string precoLimpo = txtPreco.Text
                .Replace("R$", "")
                .Replace(".", "")
                .Trim();
            return new ProdutoModel
            {
                Id = _produtoIdAtual,
                Descricao = txtDescricao.Text,
                Preco = decimal.TryParse(precoLimpo, out var p) ? p : 0,
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
            if (dataGridProdutoView1.Columns["Preco"] != null)
                dataGridProdutoView1.Columns["Preco"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            if (dataGridProdutoView1.Columns["Estoque"] != null)
                dataGridProdutoView1.Columns["Estoque"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            if (dataGridProdutoView1.Columns["Ativo"] != null)
                dataGridProdutoView1.Columns["Ativo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
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
        private void txtPreco_TextChanged(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null || string.IsNullOrEmpty(textBox.Text)) return;

            textBox.TextChanged -= txtPreco_TextChanged;

            try
            {
                // O segredo é usar o "textBox" (que é o sender) em vez de "txtPreco" diretamente
                string value = textBox.Text.Replace(",", "").Replace(".", "").Replace("R$", "").Trim();

                if (decimal.TryParse(value, out decimal result))
                {
                    textBox.Text = string.Format("{0:C2}", result / 100);
                    textBox.SelectionStart = textBox.Text.Length;
                }
            }
            catch
            {
                // Silencioso para não travar a digitação
            }

            textBox.TextChanged += txtPreco_TextChanged;
        }
        private void dataGridProdutoView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // 1. Verifica se a linha é válida
            if (e.RowIndex < 0) return;

            // 2. Formata a coluna "Preco"
            if (dataGridProdutoView1.Columns[e.ColumnIndex].Name == "Preco" ||
                dataGridProdutoView1.Columns[e.ColumnIndex].DataPropertyName == "Preco")
            {
                if (e.Value != null && decimal.TryParse(e.Value.ToString(), out decimal preco))
                {
                    e.Value = preco.ToString("C2"); // Formata como Moeda (R$ 0,00)
                    e.FormattingApplied = true;
                }
            }

            // 3. Formata a coluna "Ativo"
            if (dataGridProdutoView1.Columns[e.ColumnIndex].Name == "Ativo" ||
                dataGridProdutoView1.Columns[e.ColumnIndex].DataPropertyName == "Ativo")
            {
                if (e.Value != null)
                {
                    char status = (char)e.Value;
                    e.Value = (status == 'S') ? "Sim" : "Não";
                    e.FormattingApplied = true;
                }
            }
        }
        public void ExibirMensagem(string mensagem)
        {
            ControleDeMensagens.Informar(mensagem);
        }
        public void ExibirMensagemErro(string mensagemErro)
        {
            ControleDeMensagens.Avisar(mensagemErro);
        }
        public bool ConfirmarExclusao()
        {
            return ControleDeMensagens.Perguntar("Deseja realmente excluir este produto?");
        }
    }
}