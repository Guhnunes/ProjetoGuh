using ProjetoGuh.Features.Venda.Repository;
using ProjetoGuh.Features.Venda.View;
using System;

namespace ProjetoGuh.Features.Venda.Presenter
{
    public class VendaConsultaPresenter : IVendaConsultaPresenter
    {
        private IVendaConsultaView _view;
        private readonly IVendaRepository _repository;

        public VendaConsultaPresenter(IVendaRepository repository)
        {
            _repository = repository;
        }

        public void SetView(IVendaConsultaView view)
        {
            _view = view;

            // Assinamos os eventos que a View vai disparar
            _view.BotaoFiltrarClicado += (s, e) => Filtrar();
            _view.BotaoLimparClicado += (s, e) => LimparFiltros();
        }

        // --- ESTE É O MÉTODO QUE ESTAVA FALTANDO ---
        public void Inicializar()
        {
            try
            {
                // No carregamento inicial, buscamos as vendas do período padrão da View
                Filtrar();
            }
            catch (Exception ex)
            {
                _view.ExibirMensagem($"Erro ao inicializar consulta: {ex.Message}");
            }
        }

        public void Filtrar()
        {
            try
            {
                DateTime dataInicio = _view.ObterDataInicio();
                DateTime dataFim = _view.ObterDataFim();

                // Buscamos no repositório (O Presenter conhece o Model)
                var vendas = _repository.BuscarPorPeriodo(dataInicio, dataFim);

                // Entregamos para a View (Como object, para manter o desacoplamento)
                _view.PreencherGridVendas(vendas);
            }
            catch (Exception ex)
            {
                _view.ExibirMensagem($"Erro ao filtrar vendas: {ex.Message}");
            }
        }

        public void LimparFiltros()
        {
            _view.LimparFiltros();
            // Após limpar os campos na tela, recarregamos a grid
            Filtrar();
        }
    }
}