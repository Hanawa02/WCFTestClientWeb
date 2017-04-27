namespace WCFWebClient.Infra.Data.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Parametro",
                c => new
                    {
                        ParametroId = c.Int(nullable: false, identity: true),
                        Campo = c.String(nullable: false, maxLength: 1000),
                        Valor = c.String(nullable: false, maxLength: 1000),
                        PerfilId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ParametroId)
                .ForeignKey("dbo.Perfil", t => t.PerfilId, cascadeDelete: false)
                .Index(t => t.PerfilId);

            CreateTable(
                "dbo.Perfil",
                c => new
                    {
                        PerfilId = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 50),
                        Servico = c.String(nullable: false, maxLength: 100),
                        Capacidade = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.PerfilId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Parametro", "PerfilId", "dbo.Perfil");
            DropIndex("dbo.Parametro", new[] { "PerfilId" });
            DropTable("dbo.Perfil");
            DropTable("dbo.Parametro");
        }
    }
}
