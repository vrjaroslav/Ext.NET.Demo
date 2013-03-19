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
            };

            products.ForEach(product => data.Products.AddOrUpdate(item => item.Name, product));

            // IMPORTANT!!
            data.SaveChanges();
        }
    }
}
