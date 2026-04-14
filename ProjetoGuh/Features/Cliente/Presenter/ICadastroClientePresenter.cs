using ProjetoGuh.Features.Cliente.View;

namespace ProjetoGuh.Features.Cliente.Presenter

{
    public interface ICadastroClientePresenter
    {
        void SetView(ICadastroClienteView view);
        void Inicializar();
        void Salvar();
        void Excluir(int id);
    }
}