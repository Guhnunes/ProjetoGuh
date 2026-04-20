using ProjetoGuh.Features.Venda.Dao;
using ProjetoGuh.Features.Venda.Model;
using System.Collections.Generic;

namespace ProjetoGuh.Features.Venda.Repository
{
    public class FormaPagamentoRepository : IFormaPagamentoRepository
    {
        private readonly IFormaPagamentoDao _dao;

        // O Autofac vê este construtor e injeta o DAO automaticamente
        public FormaPagamentoRepository(IFormaPagamentoDao dao)
        {
            _dao = dao;
        }

        // Use o _dao que você recebeu no construtor
        public List<FormaPagamentoModel> Listar() => _dao.ListarFormasDePagamento();
    }
}