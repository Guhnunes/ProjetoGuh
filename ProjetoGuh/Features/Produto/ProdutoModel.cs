using System;

namespace ProjetoGuh.Features.Produto
{
    public class ProdutoModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
        public bool Ativo { get; set; }
    }
}
