using Dapper;
using ProjetoGuh.Features.Infraestrutura;
using System.Collections.Generic;
using System.Linq;
using ProjetoGuh.Features.Produto.Model;

namespace ProjetoGuh.Features.Produto.Dao
{
    public class ProdutoDao : IProdutoDao
    {
        private readonly IFabricaDeConexao _fabricaDeConexao;

        public ProdutoDao(IFabricaDeConexao fabricaDeConexao)
        {
            _fabricaDeConexao = fabricaDeConexao;
        }

        public void Incluir(ProdutoModel produto)
        {
            using (var conexao = _fabricaDeConexao.RetornarNovaConexao())
            {
                const string sql = @"INSERT INTO PRODUTO (DESCRICAO, PRECO, ESTOQUE, ATIVO) 
                                     VALUES (@Descricao, @Preco, @Estoque, @Ativo)";
                if (produto.Ativo == '\0') produto.Ativo = 'S';
                conexao.Execute(sql, produto);
            }
        }

        public void Alterar(ProdutoModel produto)
        {
            using (var conexao = _fabricaDeConexao.RetornarNovaConexao())
            {
                const string sql = @"UPDATE PRODUTO SET 
                                     DESCRICAO = @Descricao, 
                                     PRECO = @Preco, 
                                     ESTOQUE = @Estoque,
                                     ATIVO = @Ativo
                                     WHERE ID = @Id";
                conexao.Execute(sql, produto);
            }
        }

        public void Excluir(int id)
        {
            using (var conexao = _fabricaDeConexao.RetornarNovaConexao())
            {
                const string sql = "UPDATE PRODUTO SET ATIVO = 'N' WHERE ID = @id";
                conexao.Execute(sql, new { id });
            }
        }

        public ProdutoModel RetornarPorId(int id)
        {
            using (var conexao = _fabricaDeConexao.RetornarNovaConexao())
            {
                const string sql = "SELECT * FROM PRODUTO WHERE ID = @id";
                return conexao.QueryFirstOrDefault<ProdutoModel>(sql, new { id });
            }
        }

        public List<ProdutoModel> Listar()
        {
            using (var conexao = _fabricaDeConexao.RetornarNovaConexao())
            {
                const string sql = "SELECT * FROM PRODUTO ORDER BY DESCRICAO";
                return conexao.Query<ProdutoModel>(sql).ToList();
            }
        }
    }
}