using System.Collections.Generic;
using System.Linq;

namespace ProjetoGuh.Features.Cliente.Model
{
    public class ClienteModelValidator
    {
        public List<string> Validar(ClienteModel model)
        {
            var erros = new List<string>();

            // Nome: obrigatório, máximo 100 caracteres
            if (string.IsNullOrWhiteSpace(model.Nome))
                erros.Add("Nome é obrigatório.");
            else if (model.Nome.Length > 100)
                erros.Add("Nome deve ter no máximo 100 caracteres.");

            string valorLimpo = new string(model.CpfCnpj.Where(char.IsDigit).ToArray());

            if (string.IsNullOrWhiteSpace(valorLimpo))
                erros.Add("CPF/CNPJ é obrigatório.");
            else if (valorLimpo.Length != 11 && valorLimpo.Length != 14)
                erros.Add("O campo deve ter 11 dígitos (CPF) ou 14 dígitos (CNPJ).");

            if (string.IsNullOrWhiteSpace(model.Email) && !model.Email.Contains("@"))
                erros.Add("Email inválido.");

            return erros;
        }
    }
}
