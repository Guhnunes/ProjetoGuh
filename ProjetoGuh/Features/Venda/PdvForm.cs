using ProjetoGuh.Features.Cliente.Model;
using ProjetoGuh.Features.Produto.Model;
using ProjetoGuh.Features.Venda.Model;
using ProjetoGuh.Features.Venda.Presenter;
using ProjetoGuh.Features.Venda.View;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ProjetoGuh.Features.Venda
{
    public partial class PdvForm : Form, IPdvView
    {
        private readonly CadastroVendaPresenter _presenter;

        // Eventos da Interface
        public event EventHandler BotaoAdicionarItemClicado;
        public event EventHandler BotaoRemoverItemClicado;
        public event EventHandler BotaoFinalizarVendaClicado;
        public event EventHandler BotaoCancelarVendaClicado;
        public event EventHandler ProdutoSelecionadoMudou;

        public PdvForm(CadastroVendaPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);

            // Vinculação de Eventos dos Controles
            btnAdicionar.Click += (s, e) => BotaoAdicionarItemClicado?.Invoke(this, EventArgs.Empty);
            btnRemover.Click += (s, e) => BotaoRemoverItemClicado?.Invoke(this, EventArgs.Empty);
            btnFinalizar.Click += (s, e) => BotaoFinalizarVendaClicado?.Invoke(this, EventArgs.Empty);
            btnCancelar.Click += (s, e) => BotaoCancelarVendaClicado?.Invoke(this, EventArgs.Empty);

            cmbProduto.SelectedIndexChanged += (s, e) => ProdutoSelecionadoMudou?.Invoke(this, EventArgs.Empty);

            this.Load += (s, e) => _presenter.Inicializar();
        }

        #region Implementação da IPdvView

        public void PreencherComboClientes(List<ClienteModel> clientes)
        {
            cmbCliente.DataSource = null;
            cmbCliente.DataSource = clientes;
            cmbCliente.DisplayMember = "Nome";
            cmbCliente.ValueMember = "Id";
        }

        public void PreencherComboProdutos(List<ProdutoModel> produtos)
        {
            cmbProduto.DataSource = null;
            cmbProduto.DataSource = produtos;
            cmbProduto.DisplayMember = "Descricao";
            cmbProduto.ValueMember = "Id";
        }

        public void PreencherComboFormasPagamento(List<FormaPagamentoModel> formas)
        {
            cmbFormaPagamento.DataSource = null;
            cmbFormaPagamento.DataSource = formas;
            cmbFormaPagamento.DisplayMember = "Descricao";
            cmbFormaPagamento.ValueMember = "Id";
        }

        public void AtualizarGridItens(List<ItemVendaModel> itens)
        {
            dgvItens.DataSource = null;
            dgvItens.DataSource = itens;

            // Ajuste de colunas para ficar bonito
            if (dgvItens.Columns["VendaId"] != null) dgvItens.Columns["VendaId"].Visible = false;
            if (dgvItens.Columns["Id"] != null) dgvItens.Columns["Id"].Visible = false;
        }
        public ProdutoModel ObterProdutoSelecionado()
        {
            return cmbProduto.SelectedItem as ProdutoModel;
        }

        public decimal ObterQuantidade()
        {
            return (decimal)txtQuantidade.Value;
        }

        public void AtualizarPrecoUnitario(decimal preco)
        {
            txtPrecoUnitario.Text = preco.ToString("C2");
        }

        public ItemVendaModel ObterItemSelecionado()
        {
            return dgvItens.CurrentRow?.DataBoundItem as ItemVendaModel;
        }
        public int ObterClienteSelecionadoId()
        {
            // Verifica se há algum cliente selecionado no ComboBox
            if (cmbCliente.SelectedValue != null)
            {
                return (int)cmbCliente.SelectedValue;
            }

            return 0; // Ou trate como erro se o cliente for obrigatório
        }

        public int ObterFormaPagamentoId()
        {
            if (cmbFormaPagamento.SelectedValue != null)
            {
                return (int)cmbFormaPagamento.SelectedValue;
            }
            return 0;
        }
        public void AtualizarValorTotal(decimal total)
        {
            // Formata o número como moeda (R$) para exibir ao usuário
            lblTotalVenda.Text = total.ToString("C2");
        }
        public void FecharTela()
        {
            this.Close();
        }

        public string ObterObservacao() => txtObservacao.Text;

        public void AtualizarValorTotalVenda(decimal total)
        {
            lblTotalVenda.Text = total.ToString("C2");
        }

        public void LimparCamposItem()
        {
            cmbProduto.SelectedIndex = -1;
            txtQuantidade.Value = 1;
            txtPrecoUnitario.Clear();
            cmbProduto.Focus();
        }

        public void ReiniciarFormulario()
        {
            // Limpa a lista de itens da Grid
            dgvItens.DataSource = null;

            // Reseta os campos de seleção
            cmbCliente.SelectedIndex = -1;
            cmbProduto.SelectedIndex = -1;
            cmbFormaPagamento.SelectedIndex = -1;

            // Zera os valores
            txtQuantidade.Value = 1;
            txtPrecoUnitario.Clear();
            lblTotalVenda.Text = "R$ 0,00";
            txtObservacao.Clear();

            // Coloca o foco de volta no primeiro campo (geralmente o cliente ou produto)
            cmbCliente.Focus();
        }

        public void ExibirMensagem(string mensagem) => MessageBox.Show(mensagem);

        public bool ConfirmarAcao(string mensagem)
        {
            return MessageBox.Show(mensagem, "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }
        #endregion
    }
}