using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoGuh.Features.Cliente
{
    public interface ICadastroClienteView
    {
        event EventHandler BotaoSalvarFoiClicado;
        event EventHandler BotaoCancelarFoiClicado;
        ClienteModel ObterDadosDoFormulario();
        void PreencherFormulario(ClienteModel cliente);
        void LimparFormulario();
        void PreencherGrid(List<ClienteModel> clientes);
    }
}
