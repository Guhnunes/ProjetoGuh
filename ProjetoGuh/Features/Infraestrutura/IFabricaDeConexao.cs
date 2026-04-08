using System.Data;

namespace ProjetoGuh.Features.Infraestrutura
{
    public interface IFabricaDeConexao
    {
        IDbConnection RetornarNovaConexao();
    }
}
