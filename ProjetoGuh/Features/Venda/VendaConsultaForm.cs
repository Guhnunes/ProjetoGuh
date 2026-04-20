using ProjetoGuh.Features.Venda.Model;
using ProjetoGuh.Features.Venda.Presenter;
using ProjetoGuh.Features.Venda.View;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ProjetoGuh.Features.Venda
{
    // A palavra 'partial' é obrigatória aqui
    public partial class VendaConsultaForm : Form, IVendaConsultaView
    {
        private readonly VendaConsultaPresenter _presenter;

        public event EventHandler BotaoFiltrarClicado;
        public event EventHandler BotaoLimparClicado;

        public VendaConsultaForm(VendaConsultaPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.SetView(this);

            // Configurações iniciais de data
            dtpInicio.Value = DateTime.Now.AddDays(-7);
            dtpFim.Value = DateTime.Now;

            // Vinculação de eventos dos botões
            btnFiltrar.Click += (s, e) => BotaoFiltrarClicado?.Invoke(this, EventArgs.Empty);
            btnLimpar.Click += (s, e) => BotaoLimparClicado?.Invoke(this, EventArgs.Empty);
        }
        public void LimparFiltros()
        {
            dtpInicio.Value = DateTime.Now.AddDays(-7);
            dtpFim.Value = DateTime.Now;
            dgvVendas.DataSource = null;
        }

        public void PreencherGridVendas(List<VendaModel> vendas)
        {
            dgvVendas.DataSource = null;
            dgvVendas.DataSource = vendas;
            ConfigurarColunasGrid();
        }

        private void ConfigurarColunasGrid()
        {
            if (dgvVendas.Columns.Count > 0)
            {
                dgvVendas.Columns["Id"].HeaderText = "Cód. Venda";
                dgvVendas.Columns["DataVenda"].HeaderText = "Data";
                dgvVendas.Columns["ValorTotal"].HeaderText = "Total";
                dgvVendas.Columns["ValorTotal"].DefaultCellStyle.Format = "C2";
                dgvVendas.Columns["Observacao"].HeaderText = "Obs.";

                // Esconde IDs que não precisam aparecer para o usuário
                if (dgvVendas.Columns.Contains("IdCliente")) dgvVendas.Columns["IdCliente"].Visible = false;
                if (dgvVendas.Columns.Contains("IdFormaPagamento")) dgvVendas.Columns["IdFormaPagamento"].Visible = false;
            }
        }

        public DateTime ObterDataInicio() => dtpInicio.Value.Date;
        public DateTime ObterDataFim() => dtpFim.Value.Date.AddDays(1).AddSeconds(-1);

        public void ExibirMensagem(string mensagem)
        {
            MessageBox.Show(mensagem, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}