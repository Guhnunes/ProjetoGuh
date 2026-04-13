using FluentMigrator;

namespace ProjetoGuh.Features.Migrations
{
    [Migration(1)]
    public class M001_CriarTabelaCliente : Migration
    {
        public override void Up()
        {
            Create.Table("CLIENTE")
                .WithColumn("ID").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("NOME").AsString(100).NotNullable()
                .WithColumn("CPF_CNPJ").AsString(18).Nullable()
                .WithColumn("TELEFONE").AsString(20).Nullable()
                .WithColumn("EMAIL").AsString(100).Nullable()
                .WithColumn("DATA_CADASTRO").AsDateTime().NotNullable();

            //Execute.Sql("CREATE GENERATOR GEN_CLIENTE_ID"); Desnecessario pois o Identity ja faz isso e o Trigger
        }

        public override void Down() => Delete.Table("CLIENTE");
    }
}
