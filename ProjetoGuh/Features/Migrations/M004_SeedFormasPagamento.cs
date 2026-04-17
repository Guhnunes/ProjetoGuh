using FluentMigrator;

namespace ProjetoGuh.Features.Migrations
{
    [Migration(4)]
    public class M004_SeedFormasPagamento : Migration
    {
        public override void Up()
        {
            Insert.IntoTable("FORMA_PAGAMENTO").Row(new { DESCRICAO = "DINHEIRO" });
            Insert.IntoTable("FORMA_PAGAMENTO").Row(new { DESCRICAO = "CARTÃO DE CRÉDITO" });
            Insert.IntoTable("FORMA_PAGAMENTO").Row(new { DESCRICAO = "CARTÃO DE DÉBITO" });
            Insert.IntoTable("FORMA_PAGAMENTO").Row(new { DESCRICAO = "PIX" });
        }

        public override void Down()
        {
            Delete.FromTable("FORMA_PAGAMENTO").AllRows();
        }
    }
}