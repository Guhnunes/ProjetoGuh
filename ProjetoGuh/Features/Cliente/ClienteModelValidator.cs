using System.Collections.Generic;
namespace ProjetoGuh.Features.Cliente
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

            if (string.IsNullOrWhiteSpace(model.CpfCnpj))
                erros.Add("CPF/CNPJ é obrigatório.");
            else if (model.CpfCnpj.Length != 11 && model.CpfCnpj.Length != 14)
            {
                erros.Add("CPF/CNPJ inválido (deve conter 11 ou 14 dígitos).");
            }

            if (string.IsNullOrWhiteSpace(model.Email) && !model.Email.Contains("@"))
                erros.Add("Email inválido.");

            return erros;
        }
    }
}
