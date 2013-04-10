namespace Uber.Data
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderandOrderItems : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BaseItems", "UnitPrice1", c => c.Double());
            AddColumn("dbo.BaseItems", "Quantity", c => c.Int());
            AddColumn("dbo.BaseItems", "OrderDate", c => c.DateTime());
            AddColumn("dbo.BaseItems", "GrossTotal", c => c.Double());
            AddColumn("dbo.BaseItems", "Discriminator", c => c.String(maxLength: 128));
            AddColumn("dbo.BaseItems", "Product_Id", c => c.Int());
            AddColumn("dbo.BaseItems", "Order_Id", c => c.Int());
            AddForeignKey("dbo.BaseItems", "Product_Id", "dbo.Products", "Id");
            AddForeignKey("dbo.BaseItems", "Order_Id", "dbo.BaseItems", "Id");
            CreateIndex("dbo.BaseItems", "Product_Id");
            CreateIndex("dbo.BaseItems", "Order_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.BaseItems", new[] { "Order_Id" });
            DropIndex("dbo.BaseItems", new[] { "Product_Id" });
            DropForeignKey("dbo.BaseItems", "Order_Id", "dbo.BaseItems");
            DropForeignKey("dbo.BaseItems", "Product_Id", "dbo.Products");
            DropColumn("dbo.BaseItems", "Order_Id");
            DropColumn("dbo.BaseItems", "Product_Id");
            DropColumn("dbo.BaseItems", "Discriminator");
            DropColumn("dbo.BaseItems", "GrossTotal");
            DropColumn("dbo.BaseItems", "OrderDate");
            DropColumn("dbo.BaseItems", "Quantity");
            DropColumn("dbo.BaseItems", "UnitPrice1");
        }
    }
}
