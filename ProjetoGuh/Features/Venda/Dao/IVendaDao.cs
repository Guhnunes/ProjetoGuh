using ProjetoGuh.Features.Venda.Model;
using System.Collections.Generic;
using System.Data;
using System;

namespace ProjetoGuh.Features.Venda.Dao
{
    public interface IVendaDao
    {
        void Incluir(IDbTransaction transacao, VendaModel venda);
        void IncluirItem(IDbTransaction transacao, ItemVendaModel item);
        List<VendaModel> Listar();
        List<VendaModel> ListarVendasPorPeriodo(DateTime dataInicio, DateTime dataFim);
        VendaModel RetornarPorId(int id);
        List<FormaPagamentoModel> ListarFormasDePagamento();
        void Excluir(int id);
        void GravarVendaCompleta(VendaModel venda);
    }
}
