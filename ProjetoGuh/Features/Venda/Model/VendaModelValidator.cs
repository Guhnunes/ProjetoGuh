using System.Collections.Generic;

namespace ProjetoGuh.Features.Venda.Model
{
    public class VendaModelValidator
    {
        public List<string> Validar(VendaModel model)
        {
            var erros = new List<string>();

            if (model.Itens == null || model.Itens.Count == 0)
                erros.Add("A venda deve conter pelo menos um item.");
            if (model.ValorTotal <= 0)
                erros.Add("O preço de venda deve ser maior que zero.");

            return erros;
        }
    }
}
