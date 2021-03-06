using System;
using Afrimart.DataAccess.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Afrimart.DataAccess
{ 
    public class AfrimartDbContext : DbContext
    {
        public AfrimartDbContext(DbContextOptions<AfrimartDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        // public DbSet<Product> Products { get; set; }
        public DbSet<Store> Stores { get; set; }
    }
}
