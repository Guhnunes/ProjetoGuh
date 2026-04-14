using System.Collections.Generic;
using ProjetoGuh.Features.Cliente.Model;

namespace ProjetoGuh.Features.Cliente.Repository
{
    public interface IClienteRepository
    {
        void Incluir(ClienteModel cliente);
        void Alterar(ClienteModel cliente);
        void Excluir(int id);
        ClienteModel RetornarPorId(int id);
        List<ClienteModel> Listar();
    }
}
