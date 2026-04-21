using ProjetoGuh.Features.Infraestrutura;
using ProjetoGuh.Features.Produto.Presenter;
using System;
using System.Linq;
using System.Windows.Forms;

namespace ProjetoGuh.Features.Produto
{
    public partial class CadastroProdutoForm : Form, ICadastroProdutoView
    {
        public event EventHandler BotaoSalvarFoiClicado;
        public event EventHandler BotaoCancelarFoiClicado;
        public event EventHandler BotaoExcluirFoiClicado;

        private int _produtoIdAtual = 0;

        public CadastroProdutoForm(CadastroProdutoPresenter presenter)
        {
            InitializeComponent();

            // Injeção e configuração da View no Presenter
            presenter.SetView(this);

            // Assinatura de eventos
            btnSalvar.Click += (s, e) => BotaoSalvarFoiClicado?.Invoke(this, EventArgs.Empty);
            btnCancelar.Click += (s, e) => BotaoCancelarFoiClicado?.Invoke(this, EventArgs.Empty);
            btnExcluir.Click += (s, e) => BotaoExcluirFoiClicado?.Invoke(this, EventArgs.Empty);

            this.Load += (s, e) => presenter.Inicializar();
            dataGridProdutoView1.CellClick += DataGridProdutoView1_CellClick;

            // Máscara de preço
            txtPreco.TextChanged += txtPreco_TextChanged;
        }


        public int ObterId() => _produtoIdAtual;

        public string ObterDescricao() => txtDescricao.Text;

        public decimal ObterPreco()
        {
            string precoLimpo = txtPreco.Text
                .Replace("R$", "")
                .Replace(".", "")
                .Trim();

            return decimal.TryParse(precoLimpo, out var p) ? p : 0;
        }

        public int ObterEstoque() => int.TryParse(txtEstoque.Text, out var e) ? e : 0;

        public char ObterStatusAtivo() => chkAtivo.Checked ? 'S' : 'N';

        public int? ObterIdSelecionadoNaGrid()
        {
            if (dataGridProdutoView1.SelectedRows.Count > 0)
            {
                // Busca o ID direto da célula, sem precisar do objeto Model
                return Convert.ToInt32(dataGridProdutoView1.SelectedRows[0].Cells["Id"].Value);
            }
            return null;
        }

        public void PreencherCampos(int id, string descricao, decimal preco, int estoque, char ativo)
        {
            _produtoIdAtual = id;
            txtDescricao.Text = descricao;
            txtPreco.Text = preco.ToString("N2");
            txtEstoque.Text = estoque.ToString();
            chkAtivo.Checked = (ativo == 'S');
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

        public void PreencherGrid(object produtos)
        {
            dataGridProdutoView1.DataSource = null;
            dataGridProdutoView1.DataSource = produtos;

            if (dataGridProdutoView1.Columns["Id"] != null)
                dataGridProdutoView1.Columns["Id"].Visible = false;
        }

        private void DataGridProdutoView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dataGridProdutoView1.Rows[e.RowIndex];

                PreencherCampos(
                    Convert.ToInt32(row.Cells["Id"].Value),
                    row.Cells["Descricao"].Value.ToString(),
                    Convert.ToDecimal(row.Cells["Preco"].Value),
                    Convert.ToInt32(row.Cells["Estoque"].Value),
                    Convert.ToChar(row.Cells["Ativo"].Value)
                );
            }
        }

        public void ExibirMensagem(string mensagem) => ControleDeMensagens.Informar(mensagem);

        public void ExibirMensagemErro(string mensagemErro) => ControleDeMensagens.Avisar(mensagemErro);

        public bool ConfirmarExclusao() => ControleDeMensagens.Perguntar("Deseja realmente desativar este produto?");


        private void txtPreco_TextChanged(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null || string.IsNullOrEmpty(textBox.Text)) return;
            textBox.TextChanged -= txtPreco_TextChanged;
            try
            {
                string value = textBox.Text.Replace(",", "").Replace(".", "").Replace("R$", "").Trim();
                if (decimal.TryParse(value, out decimal result))
                {
                    textBox.Text = string.Format("{0:C2}", result / 100);
                    textBox.SelectionStart = textBox.Text.Length;
                }
            }
            catch { }
            textBox.TextChanged += txtPreco_TextChanged;
        }
        private void dataGridProdutoView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            if (e.RowIndex < 0) return;

            if (dataGridProdutoView1.Columns[e.ColumnIndex].Name == "Preco" ||
                dataGridProdutoView1.Columns[e.ColumnIndex].DataPropertyName == "Preco")
            {
                if (e.Value != null && decimal.TryParse(e.Value.ToString(), out decimal preco))
                {
                    e.Value = preco.ToString("C2"); // Formata como R$ 0,00
                    e.FormattingApplied = true;
                }
            }

            // 3. Formata a coluna "Ativo" para Sim/Não
            if (dataGridProdutoView1.Columns[e.ColumnIndex].Name == "Ativo" ||
                dataGridProdutoView1.Columns[e.ColumnIndex].DataPropertyName == "Ativo")
            {
                if (e.Value != null)
                {
                    // O banco costuma retornar 'S' ou 'N'
                    string status = e.Value.ToString().ToUpper();
                    e.Value = (status == "S") ? "Sim" : "Não";
                    e.FormattingApplied = true;
                }
            }
        }
    }
}