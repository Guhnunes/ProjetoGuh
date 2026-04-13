using System;
using System.Windows.Forms;

namespace ProjetoGuh.Features.Menu
{
    public interface IMenuPrincipalView
    {
        // Eventos que o Presenter vai "escutar"
        event EventHandler AbrirClienteClick;
        event EventHandler AbrirProdutoClick;
        //event EventHandler AbrirVendaClick;

        // O Presenter precisa desse método para "mandar" na View
        void AdicionarAba(string titulo, Form formFilho);
    }
}
