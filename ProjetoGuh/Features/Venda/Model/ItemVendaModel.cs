namespace ProjetoGuh.Features.Venda.Model
{
    public class ItemVendaModel
    {
        public int Id { get; set; }
        public int IdVenda { get; set; }
        public int IdProduto { get; set; }

        // Propriedade auxiliar para exibir o nome na Grid do PDV
        public string DescricaoProduto { get; set; }

        public decimal Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }

        // Propriedade calculada: facilita a vida do Presenter e da View
        public decimal ValorTotal
        {
            get => Quantidade * ValorUnitario;
            set { /* O Dapper precisa do set para mapear o banco, mas a lógica acima prevalece */ }
        }
    }
}