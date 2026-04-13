using System.Collections.Generic;
using System.Linq;
namespace ProjetoGuh.Features.Produto
{
    public class ProdutoModelValidator
    {
        public List<string> Validar(ProdutoModel model)
        {
            var erros = new List<string>();

            if (string.IsNullOrWhiteSpace(model.Descricao))
                erros.Add("Descrição do produto é obrigatória.");
            else if (model.Descricao.Length > 100)
                erros.Add("A descrição do produto deve ter no máximo 100 caracteres.");

            return erros;
        }
    }
}
