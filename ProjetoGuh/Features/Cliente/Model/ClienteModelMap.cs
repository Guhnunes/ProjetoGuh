using Dapper.FluentMap.Mapping;

namespace ProjetoGuh.Features.Cliente.Model
{
    public class ClienteModelMap : EntityMap<ClienteModel>
    {
        public ClienteModelMap()
        {
            Map(x => x.CpfCnpj).ToColumn("CPF_CNPJ");
            Map(x => x.DataCadastro).ToColumn("DATA_CADASTRO");
        }
    }
}
