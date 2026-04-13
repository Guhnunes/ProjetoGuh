using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoGuh.Features.Produto
{
    public interface IProdutoDao
    {
        void Incluir(ProdutoModel produto);
        void Alterar(ProdutoModel produto);
        void Excluir(int id);
        ProdutoModel RetornarPorId(int id);
        List<ProdutoModel> Listar();
    }
}