namespace ProjetoGuh.Features.Produto.Presenter
{
    public interface ICadastroProdutoPresenter
    {
        void SetView(ICadastroProdutoView view);
        void Inicializar();
        void Salvar();
        void Excluir(int id);
    }
}