using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoGuh.Features.Cliente
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
