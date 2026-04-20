using System;
using System.Windows.Forms;

namespace ProjetoGuh.Features.Menu.View
{
    public interface IMenuPrincipalView
    {
        // Eventos que o Presenter vai "escutar"
        event EventHandler AbrirClienteClick;
        event EventHandler AbrirProdutoClick;
        event EventHandler AbrirVendaClick;
        event EventHandler AbrirConsultaVendaClick;

        // O Presenter precisa desse método para "mandar" na View
        void AdicionarAba(string titulo, Form formFilho);
    }
}
