using System.Collections.Generic;
using ProjetoGuh.Features.Produto.Model;

namespace ProjetoGuh.Features.Produto.Repository
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
