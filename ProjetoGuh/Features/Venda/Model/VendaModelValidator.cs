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
            if(model.IdFormaPagamento == 0)
                erros.Add("A forma de pagamento deve ser selecionada.");
            if (model.IdCliente == 0)
                erros.Add("O cliente deve ser selecionado.");

            return erros;
        }
    }
}
