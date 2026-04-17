namespace ProjetoGuh.Features.Venda.Model
{
    public class ItemVendaModel
    {
        public int Id { get; set; }
        public int IdVenda { get; set; }
        public int IdProduto { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
