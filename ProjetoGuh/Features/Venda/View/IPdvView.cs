using ProjetoGuh.Features.Venda.Model;
using ProjetoGuh.Features.Cliente.Model;
using ProjetoGuh.Features.Produto.Model;
using System;
using System.Collections.Generic;

namespace ProjetoGuh.Features.Venda.View
{
    public interface IPdvView
    {
        // Eventos
        event EventHandler BotaoAdicionarItemClicado;
        event EventHandler BotaoRemoverItemClicado;
        event EventHandler BotaoFinalizarVendaClicado;
        event EventHandler BotaoCancelarVendaClicado;
        event EventHandler ProdutoSelecionadoMudou;

        // Métodos para carregar os combos
        void PreencherComboClientes(List<ClienteModel> clientes);
        void PreencherComboProdutos(List<ProdutoModel> produtos);
        void PreencherComboFormasPagamento(List<FormaPagamentoModel> formas);

        // Métodos para a Grid de Itens
        void AtualizarGridItens(List<ItemVendaModel> itens);
        ItemVendaModel ObterItemSelecionado();

        // Dados da Venda
        int ObterClienteSelecionadoId();
        int ObterFormaPagamentoId();
        ProdutoModel ObterProdutoSelecionado();
        decimal ObterQuantidade();
        string ObterObservacao();
        void ReiniciarFormulario();
        void AtualizarValorTotal(decimal total);
        void AtualizarValorTotalVenda(decimal total);
        void AtualizarPrecoUnitario(decimal preco);
        void LimparCamposItem();
        void FecharTela();
    }
}