using FluentMigrator;

namespace ProjetoGuh.Features.Migrations
{
    [Migration(3)]
    public class M003_CriarTabelasDeVenda : Migration
    {
        public override void Up()
        {
            Create.Table("FORMA_PAGAMENTO")
                .WithColumn("ID").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("DESCRICAO").AsString(100).NotNullable();
            Create.Table("VENDA")
                .WithColumn("ID").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("DATA_VENDA").AsDateTime().NotNullable()
                .WithColumn("VALOR_TOTAL").AsDecimal(10, 2).NotNullable()
                .WithColumn("OBSERVACAO").AsString(200).Nullable()
                // fkS
                .WithColumn("ID_CLIENTE").AsInt32().NotNullable().ForeignKey("FK_VENDA_CLIENTE", "CLIENTE", "ID")
                .WithColumn("ID_FORMA_PAGAMENTO").AsInt32().Nullable().ForeignKey("FK_VENDA_FORMA", "FORMA_PAGAMENTO", "ID");
            Create.Table("ITEM_VENDA")
                .WithColumn("ID_ITEM_VENDA").AsInt32().PrimaryKey().NotNullable().Identity()
                //FKS
                .WithColumn("ID_VENDA").AsInt32().NotNullable().ForeignKey("FK_ITEM_VENDA", "VENDA", "ID")
                .WithColumn("ID_PRODUTO").AsInt32().NotNullable().ForeignKey("FK_ITEM_PRODUTO", "PRODUTO", "ID")
                .WithColumn("QUANTIDADE").AsDecimal(10, 2).NotNullable()
                .WithColumn("VALOR_UNITARIO").AsDecimal(10, 2).NotNullable()
                .WithColumn("VALOR_TOTAL").AsDecimal(10, 2).NotNullable();
        }

        public override void Down()
        {
            Delete.Table("ITEM_VENDA");
            Delete.Table("VENDA");
            Delete.Table("FORMA_PAGAMENTO");
        }
    }
}