using System.Collections.Generic;
using ProjetoGuh.Features.Cliente.Model;

namespace ProjetoGuh.Features.Cliente.Dao
{
    public interface IClienteDao
    {
        void Incluir(ClienteModel cliente);
        void Alterar(ClienteModel cliente);
        void Excluir(int id);
        ClienteModel RetornarPorId(int id);
        List<ClienteModel> Listar();
    }
}
