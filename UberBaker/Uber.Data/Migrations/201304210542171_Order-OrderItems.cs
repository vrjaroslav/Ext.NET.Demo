namespace Uber.Data
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderOrderItems : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BaseItems", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.BaseItems", "Order_Id", "dbo.BaseItems");
            DropIndex("dbo.BaseItems", new[] { "Product_Id" });
            DropIndex("dbo.BaseItems", new[] { "Order_Id" });
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
            
            DropColumn("dbo.BaseItems", "UnitPrice1");
            DropColumn("dbo.BaseItems", "Quantity");
            DropColumn("dbo.BaseItems", "OrderDate");
            DropColumn("dbo.BaseItems", "GrossTotal");
            DropColumn("dbo.BaseItems", "Discriminator");
            DropColumn("dbo.BaseItems", "Product_Id");
            DropColumn("dbo.BaseItems", "Order_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BaseItems", "Order_Id", c => c.Int());
            AddColumn("dbo.BaseItems", "Product_Id", c => c.Int());
            AddColumn("dbo.BaseItems", "Discriminator", c => c.String(maxLength: 128));
            AddColumn("dbo.BaseItems", "GrossTotal", c => c.Double());
            AddColumn("dbo.BaseItems", "OrderDate", c => c.DateTime());
            AddColumn("dbo.BaseItems", "Quantity", c => c.Int());
            AddColumn("dbo.BaseItems", "UnitPrice1", c => c.Double());
            DropIndex("dbo.OrderItems", new[] { "Order_Id" });
            DropIndex("dbo.OrderItems", new[] { "Product_Id" });
            DropIndex("dbo.OrderItems", new[] { "Id" });
            DropIndex("dbo.Orders", new[] { "Id" });
            DropForeignKey("dbo.OrderItems", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.OrderItems", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.OrderItems", "Id", "dbo.BaseItems");
            DropForeignKey("dbo.Orders", "Id", "dbo.BaseItems");
            DropTable("dbo.OrderItems");
            DropTable("dbo.Orders");
            CreateIndex("dbo.BaseItems", "Order_Id");
            CreateIndex("dbo.BaseItems", "Product_Id");
            AddForeignKey("dbo.BaseItems", "Order_Id", "dbo.BaseItems", "Id");
            AddForeignKey("dbo.BaseItems", "Product_Id", "dbo.Products", "Id");
        }
    }
}
