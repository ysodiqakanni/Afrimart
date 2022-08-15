using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Afrimart.DataAccess.DataModels;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Afrimart.DataAccess.Repositories
{
    public interface IProductCategoryRepo: IBaseRepository<ProductCategory, AfrimartDbContext>
    {
        ProductCategory GetCategoryWithMostProducts();
    }
    public class ProductCategoryRepo : BaseRepository<ProductCategory, AfrimartDbContext>, IProductCategoryRepo
    {
        private AfrimartDbContext _ctx;
        public ProductCategoryRepo(AfrimartDbContext ctx): base(ctx)
        {
            _ctx = ctx;
        }

        public ProductCategory GetCategoryWithMostProducts()
        {
            return _ctx.ProductCategories.Include(x => x.Products).OrderByDescending(x => x.Products.Count)
                .First();
        }
    }
}
