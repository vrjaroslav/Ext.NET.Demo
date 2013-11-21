﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Web.Helpers;
using Uber.Core;

namespace Uber.Data
{
	public class UberContextInitializer : DropCreateDatabaseIfModelChanges<UberContext>
	{
		protected override void Seed(UberContext db)
        {
            var products = SeedProducts(db);

			var countries = SeedCountries();

			var customers = SeedCustomers(countries);

			SeedOrders(db, products, customers);

            SeedMembership(db);

			// IMPORTANT!!!
            db.SaveChanges();

        }

        private void SeedMembership(UberContext db)
        {
            // Adding Administrator
            Role adminRole = new Role
            {
                Name = "Administrator"
            };
            db.Roles.Add(adminRole);

            User admin = new User
            {
                UserName = "admin",
                Password = Crypto.HashPassword("demo"),
                Role = adminRole
            };

            db.Users.Add(admin);

            db.Profiles.Add(new UserProfile
            {
                Email = "admin@uberbaker.com",
                FirstName = "Admin",
                LastName = "",
                User = admin
            });

            // Adding Manager
            Role managerRole = new Role
            {
                Name = "Manager"
            };
            db.Roles.Add(managerRole);

            User manager = new User
            {
                UserName = "manager",
                Password = Crypto.HashPassword("demo"),
                Role = managerRole
            };

            db.Users.Add(manager);

            db.Profiles.Add(new UserProfile
            {
                Email = "manager@uberbaker.com",
                FirstName = "Manager",
                LastName = "",
                User = manager
            });

            // Adding User
            Role userRole = new Role
            {
                Name = "User"
            };
            db.Roles.Add(userRole);

            User user = new User
            {
                UserName = "user",
                Password = Crypto.HashPassword("demo"),
                Role = userRole
            };

            db.Users.Add(user);

            db.Profiles.Add(new UserProfile
            {
                Email = "user@uberbaker.com",
                FirstName = "User",
                LastName = "",
                User = user
            });
        } 

		private List<Country> SeedCountries()
		{
            return new List<Country> { 
                new Country{ Name = "Canada", ShortCode = "ca" },
                new Country{ Name = "China", ShortCode = "cn" },
                new Country{ Name = "Kazakhstan", ShortCode = "kz" },
                new Country{ Name = "Russia", ShortCode = "ru" },
                new Country{ Name = "United States", ShortCode = "us" },
            };
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

		private List<Product> SeedProducts(UberContext db)
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

			productTypes.ForEach(productType => db.ProductTypes.AddOrUpdate(item => item.ShortCode, productType));

			var products = new List<Product>
			{
				new Product {Name = "Sour Dough", UnitPrice = (decimal)2.22, ShortCode = "sourdough", ProductType = glutenfree},
				new Product {Name = "5 Grain", UnitPrice = (decimal)3.33, ShortCode = "5grain", ProductType = glutenfree},
				new Product {Name = "Baguette", UnitPrice = (decimal) 5.78, ShortCode = "baguette", ProductType = bread},
				new Product {Name = "Dark Rye", UnitPrice = (decimal)4.44, ShortCode = "darkrye", ProductType = bread},
				new Product {Name = "Pumpernickel", ShortCode = "pumpernickel", ProductType = bread},
				new Product {Name = "Corn Bread", UnitPrice = (decimal)5.55, ShortCode = "cornbread", ProductType = bread},
				new Product {Name = "Brioche", UnitPrice = (decimal)6.87, ShortCode = "brioche", ProductType = pastry},
				new Product {Name = "Focaccia", UnitPrice = (decimal)9.55,ShortCode = "focaccia", ProductType = bread},
				new Product {Name = "Croissant", UnitPrice = (decimal)4.99, ShortCode = "croissant", ProductType = pastry},
				new Product {Name = "Pain au Chocolat", UnitPrice = (decimal)1.11, ShortCode = "painauchocolat", ProductType = pastry},
				new Product {Name = "Milk Chocolate", UnitPrice = (decimal)3.41, ShortCode = "milkchocolate", ProductType = chocolate}
			};

			products.ForEach(product => db.Products.AddOrUpdate(item => item.ShortCode, product));

			return products;
		}

		private void SeedOrders(UberContext db, List<Product> products, List<Customer> customers)
		{
			Random r = new Random(DateTime.Now.Second);

			var orders = new List<Order>();
            // Generating orders for each last 31 day
		    for (int i = 1; i <= 31; i++)
		    {
                // Generating orders for each product
		        foreach (var product in products)
		        {
                    // Get random amount of orders
		            var count = r.Next(1, 5);
		            for (int j = 0; j < count; j++)
		            {
		                orders.Add(new Order
		                {
		                    OrderDate = DateTime.Now.AddDays(i * (-1)),
		                    Product = product,
		                    Quantity = r.Next(1, 50),
		                    Customer = customers[r.Next(0, customers.Count - 1)]
		                });
		            }
		        }
		    }

		    orders.ForEach(order => db.Orders.AddOrUpdate(order));
		}
	}
}
