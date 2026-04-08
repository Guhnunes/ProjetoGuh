namespace ProjetoGuh.Features.Cliente
{
    public interface ICadastroClientePresenter
    {
        void SetView(ICadastroClienteView view);
        void Inicializar();
        void Salvar();
        void Excluir(int id);
    }
}