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
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BaseItems", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Customer_Id = c.Int(),
                        DeliveryDate = c.DateTime(),
                        TotalPrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BaseItems", t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .Index(t => t.Id)
                .Index(t => t.Customer_Id);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Product_Id = c.Int(),
                        Order_Id = c.Int(),
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
            DropIndex("dbo.Orders", new[] { "Customer_Id" });
            DropIndex("dbo.Orders", new[] { "Id" });
            DropIndex("dbo.Customers", new[] { "Id" });
            DropForeignKey("dbo.ProductTypes", "Id", "dbo.BaseItems");
            DropForeignKey("dbo.Products", "Type_Id", "dbo.ProductTypes");
            DropForeignKey("dbo.Products", "Id", "dbo.BaseItems");
            DropForeignKey("dbo.OrderItems", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.OrderItems", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.OrderItems", "Id", "dbo.BaseItems");
            DropForeignKey("dbo.Orders", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Orders", "Id", "dbo.BaseItems");
            DropForeignKey("dbo.Customers", "Id", "dbo.BaseItems");
            DropTable("dbo.ProductTypes");
            DropTable("dbo.Products");
            DropTable("dbo.OrderItems");
            DropTable("dbo.Orders");
            DropTable("dbo.Customers");
            DropTable("dbo.BaseItems");
        }
    }
}
