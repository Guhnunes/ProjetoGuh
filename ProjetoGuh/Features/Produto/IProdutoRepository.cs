using System.Collections.Generic;

namespace ProjetoGuh.Features.Produto
{
    public interface IProdutoRepository
    {
        void Incluir(ProdutoModel produto);
        void Alterar(ProdutoModel produto);
        void Excluir(int id);
        ProdutoModel RetornarPorId(int id);
        List<ProdutoModel> Listar();
    }
}
