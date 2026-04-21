using ProjetoGuh.Features.Venda.View;

namespace ProjetoGuh.Features.Venda.Presenter
{
    public interface IVendaConsultaPresenter
    {
        // Vincula a View ao Presenter
        void SetView(IVendaConsultaView view);

        // Chamado no Load da tela para trazer todas as vendas ou as do dia
        void Inicializar();

        // Executa a busca baseada nos filtros de data da View
        void Filtrar();

        // Reseta os filtros na View e recarrega a lista original
        void LimparFiltros();
    }
}