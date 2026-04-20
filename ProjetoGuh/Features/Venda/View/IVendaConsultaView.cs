using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoGuh.Features.Venda.View
{
    public interface IVendaConsultaView
    {
        void PreencherGridVendas(List<VendaModel> vendas);
        DateTime ObterDataInicio();
        DateTime ObterDataFim();
        void ExibirMensagem(string mensagem);

        event EventHandler BotaoFiltrarClicado;
        event EventHandler BotaoLimparClicado;
    }
}
