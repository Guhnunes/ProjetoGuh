using System;
using System.Collections.Generic;

namespace ProjetoGuh.Features.Venda.Model
{
    public class VendaModel
    {
        public int Id { get; set; }
        public DateTime DataVenda { get; set; }
        public decimal ValorTotal { get; set; }
        public int IdFormaPagamento { get; set; }
        public string DescricaoFormaPagamento { get; set; }
        public string Observacao { get; set; }
        public List<ItemVendaModel> Itens { get; set; } = new List<ItemVendaModel>();
    }
}
