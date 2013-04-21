namespace Uber.Data
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddShortCodetoProductProductType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ShortCode", c => c.String());
            AddColumn("dbo.ProductTypes", "ShortCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductTypes", "ShortCode");
            DropColumn("dbo.Products", "ShortCode");
        }
    }
}
