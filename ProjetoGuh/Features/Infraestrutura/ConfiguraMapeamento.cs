using Dapper.FluentMap;
using ProjetoGuh.Features.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoGuh.Features.Infraestrutura
{
    public static class ConfiguraMapeamento
 {
     public static void Registrar()
     {
         FluentMapper.Initialize(config =>
         {
             config.AddMap(new ClienteModelMap());
             // Adicionar novos maps aqui conforme criar (ProdutoModelMap, etc.)
         });
     }
 }
}
