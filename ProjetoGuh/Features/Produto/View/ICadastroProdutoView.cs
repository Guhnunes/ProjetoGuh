using System;

public interface ICadastroProdutoView
{
    event EventHandler BotaoSalvarFoiClicado;
    event EventHandler BotaoCancelarFoiClicado;
    event EventHandler BotaoExcluirFoiClicado;
    //event EventHandler Load;

    // Dados Brutos
    int ObterId();
    string ObterDescricao();
    decimal ObterPreco();
    int ObterEstoque();
    char ObterStatusAtivo();

    // Injeção de dados na tela
    void PreencherCampos(int id, string descricao, decimal preco, int estoque, char ativo);
    void LimparFormulario();

    // Grid e Interação
    void PreencherGrid(object dataSource);
    int? ObterIdSelecionadoNaGrid();
    void ExibirMensagem(string mensagem);
    void ExibirMensagemErro(string mensagemErro);
    bool ConfirmarExclusao();
}