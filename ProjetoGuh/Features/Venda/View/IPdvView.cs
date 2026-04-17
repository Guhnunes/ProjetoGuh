using ProjetoGuh.Features.Venda.Model;
using System;
using System.Collections.Generic;

namespace ProjetoGuh.Features.Venda.View
{
    public interface IPdvView
    {
        // Eventos
        event EventHandler BotaoAdicionarItemFoiClicado;
        event EventHandler BotaoFinalizarVendaFoiClicado;
        event EventHandler BotaoCancelarVendaFoiClicado;
        event EventHandler AtalhoListagemFoiAcionado;   // Ex: tecla F2

        // Dados de entrada
        int ObterIdProdutoSelecionado();
        decimal ObterQuantidade();
        int ObterIdFormaPagamentoSelecionado();

        // Atualização da UI
        void AtribuirListaDeItens(List<ItemVendaModel> itens);
        void AtribuirFormasDePagamento(List<FormaPagamentoModel> formas);
        void AtualizarTotalDaVenda(decimal total);
        void LimparTela();
    }
}
