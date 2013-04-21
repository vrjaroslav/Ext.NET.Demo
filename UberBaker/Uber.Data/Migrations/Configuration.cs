namespace Uber.Data
{
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using Uber.Core;

    internal sealed class Configuration : DbMigrationsConfiguration<UberContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(UberContext data)
        {
            var bread = new ProductType { Name = "Bread", ShortCode = "bread" };
            var pastry = new ProductType { Name = "Pastry", ShortCode = "pastry" };

            var productTypes = new List<ProductType>
            {
                bread,
                pastry,
            };

            productTypes.ForEach(productType => data.ProductTypes.AddOrUpdate(item => item.ShortCode, productType));

            var products = new List<Product> { 
                new Product { Name = "Sour Dough", UnitPrice = 2.22, ShortCode = "sourdough" },
                new Product { Name = "5 Grain", UnitPrice = 3.33, ShortCode = "5grain" },
                new Product { Name = "Baguette", ShortCode = "baguette", Type = bread },
                new Product { Name = "Dark Rye", UnitPrice = 4.44, ShortCode = "darkrye", Type = bread },
                new Product { Name = "Pumpernickel", ShortCode = "pumpernickel" },
                new Product { Name = "Corn Bread", UnitPrice = 5.55, ShortCode = "cornbread" },
                new Product { Name = "Brioche", ShortCode = "brioche" },
                new Product { Name = "Focaccia", ShortCode = "focaccia" },
                new Product { Name = "Croissant", ShortCode = "croissant", Type = pastry },
                new Product { Name = "Pain au Chocolat", UnitPrice = 1.11, ShortCode = "painauchocolat", Type = pastry },
            };

            products.ForEach(product => data.Products.AddOrUpdate(item => item.ShortCode, product));

            // IMPORTANT!!
            data.SaveChanges();
        }
    }
}