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
            var products = new List<Product> { 
                new Product { Name = "Sour Dough" },
                new Product { Name = "5 Grain" },
                new Product { Name = "Baguette" },
                new Product { Name = "Dark Rye" },
                new Product { Name = "Pumpernickel" },
                new Product { Name = "Corn Bread" },
                new Product { Name = "Brioche" },
                new Product { Name = "Focaccia" }
            };

            products.ForEach(product => data.Products.AddOrUpdate(item => item.Name, product));

            var productTypes = new List<ProductType>
            {
                new ProductType { Name = "Bread" }
            };

            productTypes.ForEach(productType => data.ProductTypes.AddOrUpdate(item => item.Name, productType));

            // IMPORTANT!!
            data.SaveChanges();
        }
    }
}