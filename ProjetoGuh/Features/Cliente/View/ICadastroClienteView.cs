using System;
using System.Collections.Generic;
using ProjetoGuh.Features.Cliente.Model;

namespace ProjetoGuh.Features.Cliente.View
{
    public interface ICadastroClienteView
    {
        event EventHandler BotaoSalvarFoiClicado;
        event EventHandler BotaoCancelarFoiClicado;
        event EventHandler BotaoExcluirFoiClicado;
        ClienteModel ObterDadosDoFormulario();
        ClienteModel ObterClienteSelecionado();
        void PreencherFormulario(ClienteModel cliente);
        void LimparFormulario();
        void PreencherGrid(List<ClienteModel> clientes);
    }
}
