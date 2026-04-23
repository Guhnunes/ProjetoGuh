using System;

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
        //event EventHandler Load;

        // Métodos para carregar os combos (usando object para esconder os Models)
        void PreencherComboClientes(object clientes);
        void PreencherComboProdutos(object produtos);
        void PreencherComboFormasPagamento(object formas);

        // Métodos para a Grid de Itens (o Presenter manda a lista, a View apenas exibe)
        void AtualizarGridItens(object itens);
        int? ObterIndexItemSelecionado(); // Retorna o índice ou ID do item para o Presenter remover

        // Dados da Venda (Extração de tipos primitivos)
        int? ObterClienteSelecionadoId();
        int? ObterFormaPagamentoId();
        int? ObterProdutoSelecionadoId();
        decimal ObterQuantidade();
        string ObterObservacao();

        // Atualizações de UI (Injeção de tipos primitivos)
        void AtualizarPrecoUnitario(decimal preco);
        void AtualizarValorTotalItem(decimal total);
        void AtualizarValorTotalVenda(decimal total);

        // Comandos de Estado
        void ReiniciarFormulario();
        void LimparCamposItem();
        void FecharTela();

        // Mensagens
        void ExibirMensagem(string mensagem);
        void ExibirMensagemErro(string mensagemErro);
        bool ExibirMensagemPerguntar(string mensagemPerguntar);
    }
}