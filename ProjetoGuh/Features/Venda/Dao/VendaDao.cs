using Dapper;
using ProjetoGuh.Features.Infraestrutura;
using ProjetoGuh.Features.Venda.Model;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ProjetoGuh.Features.Venda.Dao
{
    public class VendaDao : IVendaDao
    {
        private readonly IFabricaDeConexao _fabricaDeConexao;

        public VendaDao(IFabricaDeConexao fabricaDeConexao)
        {
            _fabricaDeConexao = fabricaDeConexao;
        }
        public void GravarVendaCompleta(VendaModel venda)
        {
            using (var conexao = _fabricaDeConexao.RetornarNovaConexao())
            {
                conexao.Open();
                using (var transacao = conexao.BeginTransaction())
                {
                    try
                    {
                        // Chama o método interno para salvar a venda
                        Incluir(transacao, venda);

                        // Chama o método interno para cada item
                        foreach (var item in venda.Itens)
                        {
                            item.IdVenda = venda.Id;
                            IncluirItem(transacao, item);
                        }

                        transacao.Commit();
                    }
                    catch
                    {
                        transacao.Rollback();
                        throw;
                    }
                }
            }
        }

        // Estes métodos ficam "limpos" e focados apenas no SQL:

        public void Incluir(IDbTransaction transacao, VendaModel venda)
        {
            venda.Id = transacao.Connection.QuerySingle<int>(
        @"INSERT INTO VENDA (DATA_VENDA, VALOR_TOTAL, OBSERVACAO, ID_CLIENTE, ID_FORMA_PAGAMENTO)
          VALUES (@DataVenda, @ValorTotal, @Observacao, @IdCliente, @IdFormaPagamento)
          RETURNING ID",
        venda,
        transacao);
        }

        public void IncluirItem(IDbTransaction transacao, ItemVendaModel item)
        {
            const string sql = @"INSERT INTO ITEM_VENDA (ID_VENDA, ID_PRODUTO, QUANTIDADE, VALOR_UNITARIO, VALOR_TOTAL)
                         VALUES (@IdVenda, @IdProduto, @Quantidade, @ValorUnitario, @ValorTotal)";

            transacao.Connection.Execute(sql, item, transacao);
        }

        public List<FormaPagamentoModel> ListarFormasDePagamento()
        {
            using (var conexao = _fabricaDeConexao.RetornarNovaConexao())
            {
                return conexao.Query<FormaPagamentoModel>(
                    "SELECT id, descricao FROM Forma_Pagamento").ToList();
            }
        }

        public VendaModel RetornarPorId(int id)
        {
            using (var conexao = _fabricaDeConexao.RetornarNovaConexao())
            {
                // Corrigido de CLIENTE para VENDA
                return conexao.QueryFirstOrDefault<VendaModel>(
                    "SELECT * FROM VENDA WHERE ID = @Id", new { Id = id });
            }
        }

        public List<VendaModel> Listar()
        {
            using (var conexao = _fabricaDeConexao.RetornarNovaConexao())
            {
                // Corrigido de CLIENTE para VENDA e removido colunas que não pertencem à venda
                return conexao.Query<VendaModel>(
                    @"SELECT id, data_venda as DataVenda, valor_total as ValorTotal, observacao, 
                             id_cliente as IdCliente, id_forma_pagamento as IdFormaPagamento 
                      FROM VENDA").ToList();
            }
        }

        public void Excluir(int id)
        {
            using (var conn = _fabricaDeConexao.RetornarNovaConexao())
            {
                conn.Open();
                using (var transacao = conn.BeginTransaction())
                {
                    try
                    {
                        // Exclui itens primeiro (FK), depois a venda
                        conn.Execute("DELETE FROM ITEM_VENDA WHERE ID_VENDA = @id", new { id }, transacao);
                        conn.Execute("DELETE FROM VENDA WHERE ID = @id", new { id }, transacao);
                        
                        transacao.Commit();
                    }
                    catch
                    {
                        transacao.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}