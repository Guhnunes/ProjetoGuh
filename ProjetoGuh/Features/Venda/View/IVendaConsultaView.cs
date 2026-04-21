using System;

namespace ProjetoGuh.Features.Venda.View
{
    public interface IVendaConsultaView
    {
        // Eventos
        event EventHandler BotaoFiltrarClicado;
        event EventHandler BotaoLimparClicado;

        // Filtros (Dados Brutos)
        DateTime ObterDataInicio();
        DateTime ObterDataFim();

        // Métodos de UI
        // Usamos 'object' para que a View não precise referenciar 'VendaModel'
        void PreencherGridVendas(object vendas);

        void ExibirMensagem(string mensagem);
        void LimparFiltros();
    }
}