using System;
using System.Collections.Generic;

namespace ProjetoGuh.Features.Produto
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
    }
}
