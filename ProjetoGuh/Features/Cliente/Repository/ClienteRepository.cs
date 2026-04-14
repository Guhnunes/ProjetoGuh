using ProjetoGuh.Features.Infraestrutura;
using System.Collections.Generic;
using ProjetoGuh.Features.Cliente.Model;
using ProjetoGuh.Features.Cliente.Dao;

namespace ProjetoGuh.Features.Cliente.Repository
{
    public class ClienteRepository : BaseRepository, IClienteRepository
    {
        private readonly IClienteDao _clienteDao;

        public ClienteRepository(IClienteDao clienteDao)
        {
            _clienteDao = clienteDao;
        }

        public void Incluir(ClienteModel cliente)
        {
            ExecutarComLog(() => _clienteDao.Incluir(cliente), "ClienteRepository.Incluir");
        }

        public List<ClienteModel> Listar()
        {
            return ExecutarComLog(() => _clienteDao.Listar(), "ClienteRepository.Listar");
        }

        // Implementar os demais métodos seguindo o mesmo padrão
        public void Alterar(ClienteModel cliente)
        {
            ExecutarComLog(() => _clienteDao.Alterar(cliente), "ClienteRepository.Alterar");
        }

        public void Excluir(int id)
        {
            ExecutarComLog(() => _clienteDao.Excluir(id), "ClienteRepository.Excluir");
        }

        public ClienteModel RetornarPorId(int id)
        {
            return ExecutarComLog(() => _clienteDao.RetornarPorId(id), "ClienteRepository.RetornarPorId");
        }
    }
}
