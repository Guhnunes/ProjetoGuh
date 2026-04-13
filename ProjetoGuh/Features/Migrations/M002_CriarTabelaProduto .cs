using FluentMigrator;

namespace ProjetoGuh.Features.Migrations
{
    [Migration(2)]
    public class M002_CriarTabelaProduto : Migration
    {
        public override void Up()
        {
            Create.Table("PRODUTO")
                .WithColumn("ID").AsInt32().PrimaryKey().NotNullable()
                .WithColumn("DESCRICAO").AsString(100).NotNullable()
                .WithColumn("PRECO").AsDecimal(10, 2).NotNullable()
                .WithColumn("ESTOQUE").AsInt32().Nullable()
                .WithColumn("ATIVO").AsFixedLengthString(1).WithDefaultValue("S").Nullable();

            Execute.Sql("CREATE GENERATOR GEN_PRODUTO");
        }

        public override void Down() => Delete.Table("PRODUTO");
    }
}
