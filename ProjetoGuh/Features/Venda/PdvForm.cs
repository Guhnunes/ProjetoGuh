using ProjetoGuh.Features.Infraestrutura;
using ProjetoGuh.Features.Venda.Presenter;
using ProjetoGuh.Features.Venda.View;
using System;
using System.Windows.Forms;

namespace ProjetoGuh.Features.Venda
{
    public partial class PdvForm : Form, IPdvView
    {
        // Eventos da Interface
        public event EventHandler BotaoAdicionarItemClicado;
        public event EventHandler BotaoRemoverItemClicado;
        public event EventHandler BotaoFinalizarVendaClicado;
        public event EventHandler BotaoCancelarVendaClicado;
        public event EventHandler ProdutoSelecionadoMudou;

        public PdvForm(CadastroVendaPresenter presenter)
        {
            InitializeComponent();

            // Injeção do Presenter
            presenter.SetView(this);
            this.KeyPreview = true;

            // Vinculação de Eventos dos Controles UI
            btnAdicionar.Click += (s, e) => BotaoAdicionarItemClicado?.Invoke(this, EventArgs.Empty);
            btnRemover.Click += (s, e) => BotaoRemoverItemClicado?.Invoke(this, EventArgs.Empty);
            btnFinalizar.Click += (s, e) => BotaoFinalizarVendaClicado?.Invoke(this, EventArgs.Empty);
            btnCancelar.Click += (s, e) => BotaoCancelarVendaClicado?.Invoke(this, EventArgs.Empty);

            cmbProduto.SelectedIndexChanged += (s, e) => ProdutoSelecionadoMudou?.Invoke(this, EventArgs.Empty);

            this.Load += (s, e) => presenter.Inicializar();
        }

        #region Implementação da IPdvView

        // Note o uso de 'object' para não precisar dos 'using' de Model
        public void PreencherComboClientes(object clientes)
        {
            cmbCliente.DataSource = null;
            cmbCliente.DisplayMember = "Nome";   
            cmbCliente.ValueMember = "Id";       
            cmbCliente.DataSource = clientes;    
            cmbCliente.SelectedIndex = -1;
        }

        public void PreencherComboProdutos(object produtos)
        {
            cmbProduto.DataSource = null;
            cmbProduto.DisplayMember = "Descricao"; 
            cmbProduto.ValueMember = "Id";          
            cmbProduto.DataSource = produtos;       
            cmbProduto.SelectedIndex = -1;
        }

        public void PreencherComboFormasPagamento(object formas)
        {
            cmbFormaPagamento.DataSource = null;
            cmbFormaPagamento.DisplayMember = "Descricao"; // 1º Texto
            cmbFormaPagamento.ValueMember = "Id";          // 2º Valor (ID)
            cmbFormaPagamento.DataSource = formas;       // 3º Dados
            cmbFormaPagamento.SelectedIndex = -1;
        }

        public void AtualizarGridItens(object itens)
        {
            dgvItens.DataSource = null;
            dgvItens.AutoGenerateColumns = true; // Garante que o grid gere as colunas
            dgvItens.DataSource = itens;

            // Força o WinForms a processar as colunas geradas
            if (dgvItens.Columns.Count > 0)
            {
                if (dgvItens.Columns.Contains("DescricaoProduto"))
                {
                    dgvItens.Columns["DescricaoProduto"].HeaderText = "Produto";
                    dgvItens.Columns["DescricaoProduto"].Width = 120;
                    dgvItens.Columns["DescricaoProduto"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                if (dgvItens.Columns.Contains("IdProduto"))
                {
                    dgvItens.Columns["IdProduto"].HeaderText = "Cód. Produto";
                    dgvItens.Columns["IdProduto"].Width = 40;
                    dgvItens.Columns["IdProduto"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                if (dgvItens.Columns.Contains("Quantidade"))
                {
                    dgvItens.Columns["Quantidade"].Width = 40;
                    dgvItens.Columns["Quantidade"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                if (dgvItens.Columns.Contains("ValorUnitario"))
                {
                    dgvItens.Columns["ValorUnitario"].Width = 40;
                    dgvItens.Columns["ValorUnitario"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                if (dgvItens.Columns.Contains("ValorTotal"))
                {
                    dgvItens.Columns["ValorTotal"].Width = 40;
                    dgvItens.Columns["ValorTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                // 1. Formatação de Moeda
                FormatarColunaMoeda("ValorUnitario", "Preço Un.");
                FormatarColunaMoeda("ValorTotal", "Total Item");

                // 2. Esconder as colunas
                EsconderColuna("Id");
                EsconderColuna("IdVenda");
            }
        }

        // Métodos auxiliares para deixar o código limpo e evitar erros
        private void FormatarColunaMoeda(string nomePropriedade, string titulo)
        {
            if (dgvItens.Columns.Contains(nomePropriedade))
            {
                var col = dgvItens.Columns[nomePropriedade];
                col.HeaderText = titulo;
                col.DefaultCellStyle.Format = "C2";
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void EsconderColuna(string nomePropriedade)
        {
            if (dgvItens.Columns.Contains(nomePropriedade))
            {
                dgvItens.Columns[nomePropriedade].Visible = false;
            }
        }

        public int? ObterProdutoSelecionadoId()
        {
            // Verificação robusta: se o valor for nulo, retorna null. 
            // Se não, tenta converter para int, não importa se veio como string ou objeto.
            if (cmbProduto.SelectedValue == null) return null;

            if (int.TryParse(cmbProduto.SelectedValue.ToString(), out int id))
            {
                return id;
            }

            return null;
        }

        public decimal ObterQuantidade()
        {
            return (decimal)txtQuantidade.Value;
        }

        public void AtualizarPrecoUnitario(decimal preco)
        {
            txtPrecoUnitario.Text = preco.ToString("C2");
        }

        public int? ObterIndexItemSelecionado()
        {
            // Retorna o índice da linha na Grid para o Presenter remover da lista interna
            return dgvItens.CurrentRow?.Index;
        }

        public int? ObterClienteSelecionadoId()
        {
            return cmbCliente.SelectedValue as int?;
        }

        public int? ObterFormaPagamentoId()
        {
            return cmbFormaPagamento.SelectedValue as int?;
        }

        public string ObterObservacao() => txtObservacao.Text;

        public void AtualizarValorTotalVenda(decimal total)
        {
            lblTotalVenda.Text = total.ToString("C2");
        }

        public void AtualizarValorTotalItem(decimal total)
        {
            // Caso você tenha um campo que mostre o total do item antes de adicionar
            // lblTotalItem.Text = total.ToString("C2");
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
            dgvItens.DataSource = null;
            cmbCliente.SelectedIndex = -1;
            cmbProduto.SelectedIndex = -1;
            cmbFormaPagamento.SelectedIndex = -1;
            txtQuantidade.Value = 1;
            txtPrecoUnitario.Clear();
            lblTotalVenda.Text = "R$ 0,00";
            txtObservacao.Clear();
            cmbCliente.Focus();
        }

        public void FecharTela() => this.Close();

        public void ExibirMensagem(string mensagem) => ControleDeMensagens.Informar(mensagem);

        public void ExibirMensagemErro(string mensagemErro) => ControleDeMensagens.Avisar(mensagemErro);

        public bool ExibirMensagemPerguntar(string mensagemPerguntar) => ControleDeMensagens.Perguntar(mensagemPerguntar);

        #endregion

        // Atalhos de teclado (F4 e F5)
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F5)
            {
                BotaoFinalizarVendaClicado?.Invoke(this, EventArgs.Empty);
                return true;
            }
            if (keyData == Keys.F4)
            {
                BotaoCancelarVendaClicado?.Invoke(this, EventArgs.Empty);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}