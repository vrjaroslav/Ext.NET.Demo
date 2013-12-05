using System;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq;
using Uber.Core;

namespace Uber.Data
{
    public class UberContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<UserProfile> Profiles { get; set; }
        public DbSet<Permission> Permissions { get; set; }

		public UberContext() : base("UberContext") {}

		protected override void OnModelCreating(DbModelBuilder builder)
		{
			builder.Entity<BaseItem>().Property(item => item.Id).IsRequired();
			builder.Entity<BaseItem>().Property(item => item.DateCreated).IsRequired();
			builder.Entity<BaseItem>().Property(item => item.DateUpdated).IsRequired();

			builder.Entity<Order>().ToTable("Orders");
			builder.Entity<Product>().ToTable("Products");
			builder.Entity<ProductType>().ToTable("ProductTypes");
			builder.Entity<Customer>().ToTable("Customers");
			builder.Entity<User>().ToTable("Users");
            builder.Entity<Role>().ToTable("Roles");
            builder.Entity<Role>().HasMany<Permission>(r => r.Permisions)
                .WithOptional(p => p.Role).HasForeignKey(p => p.RoleId);
            builder.Entity<Role>().HasMany<User>(r => r.Users)
                .WithRequired(p => p.Role).HasForeignKey(p => p.RoleId);
            builder.Entity<UserProfile>().ToTable("Profiles");
			builder.Entity<Country>().ToTable("Countries");
			builder.Entity<Address>().ToTable("Addresses");
            builder.Entity<Permission>().ToTable("Permissions");
		}

		public override int SaveChanges()
		{
			var now = DateTime.Now;

			var added = this.ChangeTracker.Entries<BaseItem>().Where(c => c.State == EntityState.Added);

			if (added != null)
			{
				foreach (var entry in added)
				{
					((BaseItem)entry.Entity).SetDateCreated();
				}
			}

			var modified = this.ChangeTracker.Entries<BaseItem>().Where(c => c.State == EntityState.Modified);

            //if (modified != null)
            //{
            //    foreach (var entry in modified)
            //    {
            //        ((BaseItem)entry.Entity).SetDateUpdated();
            //    }
            //}
            base.SaveChanges();
            
			return base.SaveChanges();
		}
    }
}
