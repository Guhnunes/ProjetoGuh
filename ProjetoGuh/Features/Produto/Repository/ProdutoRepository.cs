using ProjetoGuh.Features.Infraestrutura;
using System.Collections.Generic;
using ProjetoGuh.Features.Produto.Model;
using ProjetoGuh.Features.Produto.Dao;

namespace ProjetoGuh.Features.Produto.Repository
{
    public class ProdutoRepository : BaseRepository, IProdutoRepository
    {
        private readonly IProdutoDao _produtoDao;

        public ProdutoRepository(IProdutoDao produtoDao)
        {
            _produtoDao = produtoDao;
        }

        public void Incluir(ProdutoModel produto)
        {
            _produtoDao.Incluir(produto);
        }

        public void Alterar(ProdutoModel produto) => _produtoDao.Alterar(produto);

        public void Excluir(int id) => _produtoDao.Excluir(id);

        public ProdutoModel RetornarPorId(int id) => _produtoDao.RetornarPorId(id);

        public List<ProdutoModel> Listar() => _produtoDao.Listar();
    }
}