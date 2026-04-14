namespace ProjetoGuh.Features.Produto
{
    public interface ICadastroProdutoPresenter
    {
        void SetView(ICadastroProdutoView view);
        void Inicializar();
        void Salvar();
        void Excluir(int id);
    }
}