using ProjetoGuh.Features.Venda.Model;
using System.Collections.Generic;

namespace ProjetoGuh.Features.Venda.Repository
{
    public interface IFormaPagamentoRepository
    {
        List<FormaPagamentoModel> Listar();
    }
}
