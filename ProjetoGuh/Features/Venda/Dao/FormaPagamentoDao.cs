using Dapper;
using ProjetoGuh.Features.Infraestrutura;
using ProjetoGuh.Features.Venda.Model;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoGuh.Features.Venda.Dao
{
    public class FormaPagamentoDao : IFormaPagamentoDao
    {
        private readonly IFabricaDeConexao _fabricaDeConexao;

        public FormaPagamentoDao(IFabricaDeConexao fabricaDeConexao)
        {
            _fabricaDeConexao = fabricaDeConexao;
        }

        public void Incluir(FormaPagamentoModel item)
        {
            using (var conexao = _fabricaDeConexao.RetornarNovaConexao())
            {
                const string sql = @"INSERT INTO FORMA_PAGAMENTO (DESCRICAO) 
                                     VALUES (@Descricao)";
                conexao.Execute(sql, item);
            }
        }

        public void Alterar(FormaPagamentoModel item)
        {
            using (var conexao = _fabricaDeConexao.RetornarNovaConexao())
            {
                const string sql = @"UPDATE FORMA_PAGAMENTO 
                                     SET DESCRICAO = @Descricao 
                                     WHERE ID = @Id";
                conexao.Execute(sql, item);
            }
        }

        public void Excluir(int id)
        {
            using (var conexao = _fabricaDeConexao.RetornarNovaConexao())
            {
                const string sql = "DELETE FROM FORMA_PAGAMENTO WHERE ID = @id";
                conexao.Execute(sql, new { id });
            }
        }

        public List<FormaPagamentoModel> ListarFormasDePagamento()
        {
            using (var conexao = _fabricaDeConexao.RetornarNovaConexao())
            {
                const string sql = "SELECT ID, DESCRICAO FROM FORMA_PAGAMENTO ORDER BY DESCRICAO";
                return conexao.Query<FormaPagamentoModel>(sql).ToList();
            }
        }

        public FormaPagamentoModel RetornarPorId(int id)
        {
            using (var conexao = _fabricaDeConexao.RetornarNovaConexao())
            {
                const string sql = "SELECT ID, DESCRICAO FROM FORMA_PAGAMENTO WHERE ID = @id";
                return conexao.QueryFirstOrDefault<FormaPagamentoModel>(sql, new { id });
            }
        }
    }
}