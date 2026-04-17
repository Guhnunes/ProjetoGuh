using ProjetoGuh.Features.Venda.View;

namespace ProjetoGuh.Features.Venda.Presenter
{
    public interface ICadastroVendaPresenter
    {
        void SetView(IPdvView view);
        void Inicializar();
        void Salvar();
        void Excluir(int id);
    }
}