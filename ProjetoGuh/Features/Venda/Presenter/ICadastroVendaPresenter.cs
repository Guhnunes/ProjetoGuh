using ProjetoGuh.Features.Venda.View;

namespace ProjetoGuh.Features.Venda.Presenter
{
    public interface ICadastroVendaPresenter
    {
        // Configuração inicial
        void SetView(IPdvView view);
        void Inicializar();

        // Lógica do PDV (Itens em memória)
        void AdicionarItem();
        //void RemoverItem();
        void AtualizarPrecoProduto();

        // Persistência e Controle
        void FinalizarVenda(); // Substitui o antigo 'Salvar'
        void CancelarVenda();
        void Excluir(int id); // Para cancelamento de vendas já salvas (opcional)
    }
}