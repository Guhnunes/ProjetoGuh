using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoGuh.Features.Cliente
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
