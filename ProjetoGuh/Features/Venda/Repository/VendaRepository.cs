using ProjetoGuh.Features.Infraestrutura;
using ProjetoGuh.Features.Venda.Dao;
using ProjetoGuh.Features.Venda.Model;
using System.Collections.Generic;
using System.Data;

namespace ProjetoGuh.Features.Venda.Repository
{
    public class VendaRepository : BaseRepository, IVendaRepository
    {
        private readonly IVendaDao _vendaDao;
        public VendaRepository(IVendaDao vendaDao)
        {
            _vendaDao = vendaDao;
        }
        public void Incluir(IDbTransaction transacao, VendaModel venda)
        {
            _vendaDao.Incluir(transacao, venda);
        }
        public void IncluirItem(IDbTransaction transacao, ItemVendaModel venda)
        {
            _vendaDao.IncluirItem(transacao, venda);
        }
        public VendaModel RetornarPorId(int id) => _vendaDao.RetornarPorId(id);
        public List<VendaModel> Listar() => _vendaDao.Listar();
        public List<FormaPagamentoModel> ListarFormasDePagamento() => _vendaDao.ListarFormasDePagamento();
        public void Excluir(int id)
        {
            _vendaDao.Excluir(id);
        }
        public void GravarVendaCompleta(VendaModel venda)
        {
            _vendaDao.GravarVendaCompleta(venda);
        }
    }
}