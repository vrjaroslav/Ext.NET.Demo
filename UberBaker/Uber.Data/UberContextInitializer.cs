using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using Uber.Core;

namespace Uber.Data
{
	public class UberContextInitializer : DropCreateDatabaseIfModelChanges<UberContext>
	{
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
                new Product { Name = "Baguette", ShortCode = "baguette", ProductType = bread },
                new Product { Name = "Dark Rye", UnitPrice = 4.44, ShortCode = "darkrye", ProductType = bread },
                new Product { Name = "Pumpernickel", ShortCode = "pumpernickel" },
                new Product { Name = "Corn Bread", UnitPrice = 5.55, ShortCode = "cornbread" },
                new Product { Name = "Brioche", ShortCode = "brioche" },
                new Product { Name = "Focaccia", ShortCode = "focaccia" },
                new Product { Name = "Croissant", ShortCode = "croissant", ProductType = pastry },
                new Product { Name = "Pain au Chocolat", UnitPrice = 1.11, ShortCode = "painauchocolat", ProductType = pastry },
            };

            products.ForEach(product => data.Products.AddOrUpdate(item => item.ShortCode, product));

			var orderItemBaguette = new OrderItem 
			{
				UnitPrice = 10,
				Quantity = 50,
				Product = products[2]
			};

			var orderItemCroissant = new OrderItem 
			{
				UnitPrice = 10,
				Quantity = 50,
				Product = products[0]
			};

			var orderItems = new List<OrderItem>
			{
				orderItemBaguette, orderItemCroissant
			};

			orderItems.ForEach(orderItem => data.OrderItems.AddOrUpdate(orderItem));

			var orderBread = new Order 
			{
				OrderDate = new DateTime(2013, 10, 12),
				OrderItems = new List<OrderItem>
				{
					orderItemBaguette
				}
			};

			var orderPastry = new Order
			{
				OrderDate = new DateTime(2013, 10, 1),
				OrderItems = new List<OrderItem>
				{
					orderItemCroissant
				}
			};

			var orders = new List<Order> { orderBread, orderPastry };

			orders.ForEach(order => data.Orders.AddOrUpdate(order));

            // IMPORTANT!!
            data.SaveChanges();
        }
	}
}
