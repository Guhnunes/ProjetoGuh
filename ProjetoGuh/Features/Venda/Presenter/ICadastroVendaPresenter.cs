using ProjetoGuh.Features.Venda.View;

namespace ProjetoGuh.Features.Venda.Presenter
{
    public interface ICadastroVendaPresenter
    {
        // Configuração inicial e carga de combos (Clientes, Produtos, Formas de Pagto)
        void SetView(IPdvView view);
        void Inicializar();

        // Lógica de manipulação de Itens (Gerencia a lista temporária em memória)
        void AdicionarItem();
        void RemoverItem(); // Agora implementado para o fluxo completo

        // Disparado quando o usuário muda o Produto no ComboBox da View
        void AtualizarDadosProdutoSelecionado();

        // Persistência no Banco de Dados
        void FinalizarVenda();

        // Limpa a tela e a lista de itens atual
        void CancelarVenda();

        // Caso precise excluir uma venda já realizada (opcional para o PDV)
        void Excluir(int id);
    }
}