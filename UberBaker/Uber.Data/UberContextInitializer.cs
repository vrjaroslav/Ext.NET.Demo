using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using Uber.Core;

namespace Uber.Data
{
	public class UberContextInitializer : DropCreateDatabaseAlways<UberContext>
	{
		protected override void Seed(UberContext data)
        {
            var products = SeedProducts(data);

			SeedOrders(data, products);

			// IMPORTANT!!
            data.SaveChanges();
        }

		private static List<Product> SeedProducts(UberContext data)
		{
			var bread = new ProductType {Name = "Bread", ShortCode = "bread"};
			var pastry = new ProductType {Name = "Pastry", ShortCode = "pastry"};
			var pita = new ProductType {Name = "Pita", ShortCode = "pita"};
			var glutenfree = new ProductType {Name = "Gluten Free", ShortCode = "glutenfree"};

			var productTypes = new List<ProductType>
				{
					bread,
					pastry,
					pita,
					glutenfree
				};

			productTypes.ForEach(productType => data.ProductTypes.AddOrUpdate(item => item.ShortCode, productType));

			var products = new List<Product>
				{
					new Product {Name = "Sour Dough", UnitPrice = 2.22, ShortCode = "sourdough", ProductType = glutenfree},
					new Product {Name = "5 Grain", UnitPrice = 3.33, ShortCode = "5grain", ProductType = glutenfree},
					new Product {Name = "Baguette", ShortCode = "baguette", ProductType = bread},
					new Product {Name = "Dark Rye", UnitPrice = 4.44, ShortCode = "darkrye", ProductType = bread},
					new Product {Name = "Pumpernickel", ShortCode = "pumpernickel", ProductType = bread},
					new Product {Name = "Corn Bread", UnitPrice = 5.55, ShortCode = "cornbread", ProductType = bread},
					new Product {Name = "Brioche", ShortCode = "brioche", ProductType = pastry},
					new Product {Name = "Focaccia", ShortCode = "focaccia", ProductType = bread},
					new Product {Name = "Croissant", ShortCode = "croissant", ProductType = pastry},
					new Product {Name = "Pain au Chocolat", UnitPrice = 1.11, ShortCode = "painauchocolat", ProductType = pastry},
				};

			products.ForEach(product => data.Products.AddOrUpdate(item => item.ShortCode, product));
			return products;
		}

		private static void SeedOrders(UberContext data, List<Product> products)
		{
			Random r = new Random(20);

			var orders = new List<Order>();

			foreach (var product in products)
				orders.Add(new Order
					{
						OrderDate = DateTime.Now.AddDays(r.Next(-15, 15)),
						Product = product,
						Quantity = r.Next(1, 50)
					});

			orders.ForEach(order => data.Orders.AddOrUpdate(order));
		}
	}
}
