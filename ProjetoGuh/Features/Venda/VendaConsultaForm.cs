using ProjetoGuh.Features.Venda.Presenter;
using ProjetoGuh.Features.Venda.View;
using System;
using System.Windows.Forms;

namespace ProjetoGuh.Features.Venda
{
    public partial class VendaConsultaForm : Form, IVendaConsultaView
    {
        // Eventos da Interface
        public event EventHandler BotaoFiltrarClicado;
        public event EventHandler BotaoLimparClicado;

        public VendaConsultaForm(VendaConsultaPresenter presenter)
        {
            InitializeComponent();

            // Vinculação com o Presenter
            presenter.SetView(this);

            // Configurações iniciais de data (UI)
            dtpInicio.Value = DateTime.Now.AddDays(-7);
            dtpFim.Value = DateTime.Now;

            // Vinculação de eventos dos botões
            btnFiltrar.Click += (s, e) => BotaoFiltrarClicado?.Invoke(this, EventArgs.Empty);
            btnLimpar.Click += (s, e) => BotaoLimparClicado?.Invoke(this, EventArgs.Empty);

            // Carga inicial
            this.Load += (s, e) => presenter.Inicializar();
        }

        #region Implementação da IVendaConsultaView

        public DateTime ObterDataInicio() => dtpInicio.Value.Date;

        public DateTime ObterDataFim() => dtpFim.Value.Date.AddDays(1).AddSeconds(-1);

        public void PreencherGridVendas(object vendas)
        {
            dgvVendas.DataSource = null;
            dgvVendas.DataSource = vendas;
            ConfigurarColunasGrid();
        }

        public void LimparFiltros()
        {
            dtpInicio.Value = DateTime.Now.AddDays(-7);
            dtpFim.Value = DateTime.Now;
            dgvVendas.DataSource = null;
        }

        public void ExibirMensagem(string mensagem)
        {
            // Usando o MessageBox padrão ou sua classe de ControleDeMensagens
            MessageBox.Show(mensagem, "Consulta de Vendas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        private void ConfigurarColunasGrid()
        {
            // Verificação de segurança para o WinForms
            if (dgvVendas.Columns.Count == 0) return;

            // Formatação amigável das colunas
            if (dgvVendas.Columns.Contains("Id")) dgvVendas.Columns["Id"].HeaderText = "Cód. Venda";
            if (dgvVendas.Columns.Contains("DataVenda")) dgvVendas.Columns["DataVenda"].HeaderText = "Data";

            if (dgvVendas.Columns.Contains("ValorTotal"))
            {
                dgvVendas.Columns["ValorTotal"].HeaderText = "Total";
                dgvVendas.Columns["ValorTotal"].DefaultCellStyle.Format = "C2";
                dgvVendas.Columns["ValorTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            if (dgvVendas.Columns.Contains("Observacao")) dgvVendas.Columns["Observacao"].HeaderText = "Obs.";

            // Esconde dados técnicos que o usuário não precisa ver
            string[] colunasParaEsconder = { "IdCliente", "IdFormaPagamento", "Itens" };
            foreach (var nomeColuna in colunasParaEsconder)
            {
                if (dgvVendas.Columns.Contains(nomeColuna))
                    dgvVendas.Columns[nomeColuna].Visible = false;
            }
        }
    }
}