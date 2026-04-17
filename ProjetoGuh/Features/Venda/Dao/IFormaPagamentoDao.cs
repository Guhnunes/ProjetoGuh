using ProjetoGuh.Features.Venda.Model;
using System.Collections.Generic;

namespace ProjetoGuh.Features.Venda.Dao
{
    public interface IFormaPagamentoDao
    {
        void Incluir(FormaPagamentoModel item);
        void Alterar(FormaPagamentoModel item);
        List<FormaPagamentoModel> Listar();
        FormaPagamentoModel RetornarPorId(int id);
        void Excluir(int id);
    }
}
