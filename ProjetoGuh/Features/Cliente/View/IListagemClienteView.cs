using System;

namespace ProjetoGuh.Features.Cliente.View
{
    public interface IListagemClienteView
    {
        event EventHandler BotaoNovoFoiClicado;
        event EventHandler BotaoEditarFoiClicado;
        event EventHandler BotaoExcluirFoiClicado;

        int? ObterIdSelecionado();
        void AtribuirListaDeClientes(object listaDeClientes);
        void ExibirMensagem(string mensagem);
        bool ConfirmarExclusao();
    }
}