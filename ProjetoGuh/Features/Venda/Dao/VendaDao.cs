using Dapper;
using ProjetoGuh.Features.Infraestrutura;
using ProjetoGuh.Features.Venda.Model;
using System.Collections.Generic;
using System.Data;

namespace ProjetoGuh.Features.Venda.Dao
{
    public class VendaDao : IVendaDao
    {
        private readonly IFabricaDeConexao _fabricaDeConexao;

        public VendaDao(IFabricaDeConexao fabricaDeConexao)
        {
            _fabricaDeConexao = fabricaDeConexao;
        }

        public void Incluir(IDbTransaction transacao, VendaModel venda)
        {
            using (var conexao = _fabricaDeConexao.RetornarNovaConexao())
            {
                conexao.Open();
                conexao.Execute(@"INSERT INTO VENDA (DATA_VENDA, VALOR_TOTAL, OBSERVACAO, ID_CLIENTE, ID_FORMA_PAGAMENTO)
                              VALUES (@DataVenda, @ValorTotal, @Observacao, @IdCliente, @IdFormaPagamento)", venda);
            }
        }
        public void IncluirItem(IDbTransaction transacao, ItemVendaModel item)
        {
            using (var conexao = _fabricaDeConexao.RetornarNovaConexao())
            {
                conexao.Open();
                conexao.Execute(@"INSERT INTO ITEM_VENDA (ID_VENDA, ID_PRODUTO, QUANTIDADE, VALOR_UNITARIO, VALOR_TOTAL)
                              VALUES (@ProdutoId, @Quantidade, @ValorUnitario, @ValorTotal)", item);
            }
        }
        public List<FormaPagamentoModel> ListarFormasDePagamento()
        {
            using (var conexao = _fabricaDeConexao.RetornarNovaConexao())
            {
                var sql = @"SELECT 
                        id, 
                        descricao
                    FROM Forma_Pagamento";
                conexao.Open();
                return conexao.Query<FormaPagamentoModel>(sql).AsList();
            }
        }
        public VendaModel RetornarPorId(int id)
        {
            using (var conexao = _fabricaDeConexao.RetornarNovaConexao())
            {
                conexao.Open();
                return conexao.QueryFirstOrDefault<VendaModel>(
                    "SELECT * FROM CLIENTE WHERE ID = @Id", new { Id = id });
            }
        }
        public List<VendaModel> Listar()
        {
            using (var conexao = _fabricaDeConexao.RetornarNovaConexao())
            {
                var sql = @"SELECT 
                        id, 
                        nome, 
                        cpf_cnpj AS CpfCnpj, 
                        telefone, 
                        email, 
                        data_cadastro AS DataCadastro 
                    FROM Cliente"; //Tive que colocar um Alias por conta do underline. O dapper não consegue mapear
                conexao.Open();
                return conexao.Query<VendaModel>(sql).AsList();
            }
        }
    }
}
