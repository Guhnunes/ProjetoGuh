using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoGuh.Features.Produto
{
    public interface IProdutoView
    {
        event EventHandler SalvarClick;
        string Descricao { get; set; }
        decimal Preco { get; set; }
        int Estoque { get; set; }

        void MostrarMensagem(string msg);
        void LimparCampos();
    }
}
