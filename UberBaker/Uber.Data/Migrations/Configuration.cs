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
                new Product { Name = "Sour Dough", ShortCode="sourdough" },
                new Product { Name = "5 Grain", ShortCode="5grain" },
                new Product { Name = "Baguette", ShortCode="baguette" },
                new Product { Name = "Dark Rye", ShortCode="darkrye" },
                new Product { Name = "Pumpernickel", ShortCode="pumpernickel" },
                new Product { Name = "Corn Bread", ShortCode="cornbread" },
                new Product { Name = "Brioche", ShortCode="brioche" },
                new Product { Name = "Focaccia", ShortCode="focaccia" }
            };

            products.ForEach(product => data.Products.AddOrUpdate(item => item.ShortCode, product));

            var productTypes = new List<ProductType>
            {
                new ProductType { Name = "Bread", ShortCode="bread" }
            };

            productTypes.ForEach(productType => data.ProductTypes.AddOrUpdate(item => item.ShortCode, productType));

            // IMPORTANT!!
            data.SaveChanges();
        }
    }
}