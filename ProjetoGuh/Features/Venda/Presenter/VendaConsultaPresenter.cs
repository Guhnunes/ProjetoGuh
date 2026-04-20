using ProjetoGuh.Features.Venda.Dao;
using ProjetoGuh.Features.Venda.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoGuh.Features.Venda.Presenter
{
    public class VendaConsultaPresenter : IVendaConsultaPresenter
    {
        private IVendaConsultaView _view;
        private readonly VendaDao _repository;

        public VendaConsultaPresenter(VendaDao repository)
        {
            _repository = repository;
        }

        public void SetView(IVendaConsultaView view)
        {
            _view = view;
            _view.BotaoFiltrarClicado += (s, e) => CarregarVendas();
            _view.BotaoLimparClicado += (s, e) => {
                // Lógica de limpeza
                _view.PreencherGridVendas(new List<VendaModel>());
                // Se você criou o método LimparFiltros na interface, chame-o aqui
            };
        }

        public void CarregarVendas()
        {
            var dataInicio = _view.ObterDataInicio();
            var dataFim = _view.ObterDataFim();

            // Aqui você usará o Dapper no seu DAO para filtrar por data
            var vendas = _repository.ListarVendasPorPeriodo(dataInicio, dataFim);
            _view.PreencherGridVendas(vendas);
        }
    }
}
