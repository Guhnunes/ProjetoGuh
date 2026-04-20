using ProjetoGuh.Features.Venda.Model;
using System;
using System.Collections.Generic;

public class VendaModel
{
    public VendaModel()
    {
        Itens = new List<ItemVendaModel>();
        DataVenda = DateTime.Now;
    }

    public int Id { get; set; }
    public DateTime DataVenda { get; set; }
    public int IdCliente { get; set; }
    public int IdFormaPagamento { get; set; }
    public decimal ValorTotal { get; set; }
    public string Observacao { get; set; }
    public List<ItemVendaModel> Itens { get; set; } 
}