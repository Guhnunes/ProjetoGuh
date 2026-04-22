using FluentMigrator;

namespace ProjetoGuh.Features.Migrations
{
    [Migration(5)]
    public class M005_AlterarTabelaCliente : Migration
    {
        public override void Up()
        {
            Alter.Table("CLIENTE")
                .AddColumn("CEP").AsString(10).Nullable()
                .AddColumn("LOGRADOURO").AsString(100).Nullable()
                .AddColumn("NUMERO").AsString(10).Nullable()
                .AddColumn("BAIRRO").AsString(50).Nullable()
                .AddColumn("CIDADE").AsString(50).Nullable()
                .AddColumn("UF").AsString(2).Nullable();
        }

        public override void Down() => Delete.Table("CLIENTE");
    }
}
