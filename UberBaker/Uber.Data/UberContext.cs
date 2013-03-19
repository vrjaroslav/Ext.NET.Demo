using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using Uber.Core;

namespace Uber.Data
{
    public class UberContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<BaseItem>().Property(item => item.Id).IsRequired();
            builder.Entity<BaseItem>().Property(item => item.DateCreated).IsRequired();
            builder.Entity<BaseItem>().Property(item => item.DateUpdated).IsRequired();

            builder.Entity<Product>().ToTable("Products");
            builder.Entity<ProductType>().ToTable("ProductTypes");
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

            if (modified != null)
            {
                foreach (var entry in modified)
                {
                    ((BaseItem)entry.Entity).SetDateUpdated();
                }
            }

            return base.SaveChanges();
        }
    }
}
