using System;
using System.Collections.Generic;
using ProjetoGuh.Features.Produto.Model;

namespace ProjetoGuh.Features.Produto.View
{
    public interface ICadastroProdutoView
    {
        event EventHandler BotaoSalvarFoiClicado;
        event EventHandler BotaoCancelarFoiClicado;
        event EventHandler BotaoExcluirFoiClicado;
        ProdutoModel ObterDadosDoFormulario();
        ProdutoModel ObterProdutoSelecionado();
        void PreencherFormulario(ProdutoModel produto);
        void LimparFormulario();
        void PreencherGrid(List<ProdutoModel> produtos);
        bool Ativo { get; set; }
        void ExibirMensagem(string mensagem);
        void ExibirMensagemErro(string mensagemErro);
        bool ConfirmarExclusao();
    }
}
