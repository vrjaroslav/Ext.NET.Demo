namespace Uber.Data
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
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
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        GrossTotal = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BaseItems", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Product_Id = c.Int(),
                        Order_Id = c.Int(),
                        UnitPrice = c.Double(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BaseItems", t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .Index(t => t.Id)
                .Index(t => t.Product_Id)
                .Index(t => t.Order_Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Type_Id = c.Int(),
                        Name = c.String(),
                        ShortCode = c.String(),
                        Description = c.String(),
                        UnitPrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BaseItems", t => t.Id)
                .ForeignKey("dbo.ProductTypes", t => t.Type_Id)
                .Index(t => t.Id)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.ProductTypes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        ShortCode = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BaseItems", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.ProductTypes", new[] { "Id" });
            DropIndex("dbo.Products", new[] { "Type_Id" });
            DropIndex("dbo.Products", new[] { "Id" });
            DropIndex("dbo.OrderItems", new[] { "Order_Id" });
            DropIndex("dbo.OrderItems", new[] { "Product_Id" });
            DropIndex("dbo.OrderItems", new[] { "Id" });
            DropIndex("dbo.Orders", new[] { "Id" });
            DropForeignKey("dbo.ProductTypes", "Id", "dbo.BaseItems");
            DropForeignKey("dbo.Products", "Type_Id", "dbo.ProductTypes");
            DropForeignKey("dbo.Products", "Id", "dbo.BaseItems");
            DropForeignKey("dbo.OrderItems", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.OrderItems", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.OrderItems", "Id", "dbo.BaseItems");
            DropForeignKey("dbo.Orders", "Id", "dbo.BaseItems");
            DropTable("dbo.ProductTypes");
            DropTable("dbo.Products");
            DropTable("dbo.OrderItems");
            DropTable("dbo.Orders");
            DropTable("dbo.BaseItems");
        }
    }
}
