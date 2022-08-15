using System;
using System.Linq;
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
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFile> ProductFiles { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<ShoppingCart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<ShopperProfile> ShopperProfiles { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
