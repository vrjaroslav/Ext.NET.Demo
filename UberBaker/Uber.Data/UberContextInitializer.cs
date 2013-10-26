using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using Uber.Core;

namespace Uber.Data
{
	public class UberContextInitializer : DropCreateDatabaseAlways<UberContext>
	{
		protected override void Seed(UberContext data)
        {
            var products = this.SeedProducts(data);

			var countries = SeedCountries();

			var customers = SeedCustomers(countries);

			SeedOrders(data, products, customers);

			// IMPORTANT!!
            data.SaveChanges();
        }

		private List<Country> SeedCountries()
		{
			string[] countries = new string[] { "U.S.", "Canada", "Russia", "UK", "Kazakhstan", "China" };
			
			var result = new List<Country>();

			foreach (var country in countries)
			{
				result.Add(new Country
				{
					Name = country
				});
			}

			return result;
		}

		private List<Customer> SeedCustomers(List<Country> countries)
		{
			const int citiesCount = 7;

			string[] firstNames = new[] { "Aaron", "Abdul", "Bob", "Cindy", "Daniel", "Evan", "George", "Ivan", "Karl", "Yann"};
			string[] lastNames = new[] { "Abbott", "Black", "Norriss", "King", "Smith", "White" };
			string[] cities = new string[citiesCount] { "New York", "Toronto", "Jacksonville", "Columbus", "Montreal", "Edmonton", "Vancouver" };
			string[] states = new string[citiesCount] { "NY", "ON", "FL", "OH", "QC", "AB", "BC" };
			string[] zipCodes = new string[citiesCount] { "10001", "M4B 1C2", "32218", "43085", "H1A 5J4", "T5A 0E2", "V5K 1A8" };
			var customers = new List<Customer>();

			var r = new Random(30);

			foreach (var lastName in lastNames)
			{
				foreach (var firstName in firstNames)
				{
					customers.Add(new Customer
					{
						FirstName = firstName,
						LastName = lastName,
						Company = lastName + " Inc.",
						ContactPhone = "111-222-3444",
						CellPhone = "555-666-7777",
						BillingAddress = new Address
						{
							Country = countries[r.Next(0, countries.Count - 1)],
							City = cities[r.Next(0, 6)],
							State = states[citiesCount - 1],
							StreetAddress = string.Format("{0} {1} Av.", r.Next(1, 999), r.Next(1, 30)),
							ZipCode = zipCodes[citiesCount - 1]
						},
						ShippingAddress = new Address
						{
							Country = countries[r.Next(0, countries.Count - 1)],
							City = cities[r.Next(0, 6)],
							State = states[citiesCount - 1],
							StreetAddress = string.Format("{0} {1} Av.", r.Next(1, 999), r.Next(1, 30)),
							ZipCode = zipCodes[citiesCount - 1]
						},
						Email = string.Format("{0}@{1}.com", firstName.ToLowerInvariant(), lastName.ToLowerInvariant())
					});
				}
			}

			return customers;
		}

		private List<Product> SeedProducts(UberContext data)
		{
			var bread = new ProductType {Name = "Bread", ShortCode = "bread"};
			var pastry = new ProductType {Name = "Pastry", ShortCode = "pastry"};
            var glutenfree = new ProductType { Name = "Gluten Free", ShortCode = "glutenfree" };
            var chocolate = new ProductType { Name = "Chocolate", ShortCode = "chocolate" };

			var productTypes = new List<ProductType>
			{
				bread,
				pastry,
				glutenfree,
                chocolate
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

		private void SeedOrders(UberContext data, List<Product> products, List<Customer> customers)
		{
			Random r = new Random(20);

			var orders = new List<Order>();

			foreach (var product in products)
			{
				orders.Add(new Order
					{
						OrderDate = DateTime.Now.AddDays(r.Next(-15, 15)),
						Product = product,
						Quantity = r.Next(1, 50),
						Customer = customers[r.Next(0, customers.Count - 1)]
					});
			}

			orders.ForEach(order => data.Orders.AddOrUpdate(order));
		}
	}
}
