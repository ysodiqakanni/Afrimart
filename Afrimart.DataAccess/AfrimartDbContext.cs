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
    }
}
