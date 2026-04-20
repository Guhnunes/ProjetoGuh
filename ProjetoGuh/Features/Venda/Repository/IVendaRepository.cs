using ProjetoGuh.Features.Venda.Model;
using System.Collections.Generic;
using System.Data;

namespace ProjetoGuh.Features.Venda.Repository
{
    public interface IVendaRepository
    {
        void Incluir(IDbTransaction transacao, VendaModel venda);
        void IncluirItem(IDbTransaction transacao, ItemVendaModel item);
        List<VendaModel> Listar();
        VendaModel RetornarPorId(int id);
        List<FormaPagamentoModel> ListarFormasDePagamento();
        void Excluir(int id);
        void GravarVendaCompleta(VendaModel venda);
    }
}
