using Dapper.FluentMap.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoGuh.Features.Cliente
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
