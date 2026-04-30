namespace TechnicalTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Personas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.String(nullable: false, maxLength: 30),
                        Nombres = c.String(nullable: false, maxLength: 150),
                        ApellidoUno = c.String(nullable: false, maxLength: 150),
                        ApellidoDos = c.String(nullable: false),
                        Hobby = c.String(nullable: false, maxLength: 150),
                        FechaNacimiento = c.DateTime(nullable: false),
                        Correo = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Personas");
        }
    }
}
