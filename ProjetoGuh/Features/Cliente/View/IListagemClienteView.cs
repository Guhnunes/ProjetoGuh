using System;
using System.Collections.Generic;
using ProjetoGuh.Features.Cliente.Model;

namespace ProjetoGuh.Features.Cliente.View
{
    public interface IListagemClienteView
    {
        event EventHandler BotaoNovoFoiClicado;
        event EventHandler BotaoEditarFoiClicado;
        event EventHandler BotaoExcluirFoiClicado;
        int ObterIdSelecionado();
        void AtribuirListaDeClientes(List<ClienteModel> clientes);
    }
}
