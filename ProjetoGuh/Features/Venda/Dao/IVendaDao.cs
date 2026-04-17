using ProjetoGuh.Features.Venda.Model;
using System.Collections.Generic;
using System.Data;

namespace ProjetoGuh.Features.Venda.Dao
{
    public interface IVendaDao
    {
        void Incluir(IDbTransaction transacao, VendaModel venda);
        void IncluirItem(IDbTransaction transacao, ItemVendaModel item);
        List<VendaModel> Listar();
        VendaModel RetornarPorId(int id);
        List<FormaPagamentoModel> ListarFormasDePagamento();
    }
}
