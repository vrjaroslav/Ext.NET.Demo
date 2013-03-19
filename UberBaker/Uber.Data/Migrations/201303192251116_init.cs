namespace Uber.Data
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BaseItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        UnitPrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BaseItems", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.ProductTypes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BaseItems", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.ProductTypes", new[] { "Id" });
            DropIndex("dbo.Products", new[] { "Id" });
            DropForeignKey("dbo.ProductTypes", "Id", "dbo.BaseItems");
            DropForeignKey("dbo.Products", "Id", "dbo.BaseItems");
            DropTable("dbo.ProductTypes");
            DropTable("dbo.Products");
            DropTable("dbo.BaseItems");
        }
    }
}
