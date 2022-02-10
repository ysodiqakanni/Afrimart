using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Afrimart.DataAccess.DataModels;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Afrimart.DataAccess.Repositories
{ 
    public interface IProductRepo : IBaseRepository<Product, AfrimartDbContext>
    {
        List<Product> GetTrendingProducts(int count);
        List<Product> GetBestSellingProductsByCategory(int categoryId, int count);
        List<Product> GetNewestProducts(int count);
        Product GetProductWithCategoryAndFilesByPSIN(string psin);
        Product GetProductWithCategoryReviewsAndFilesByPSIN(string psin);
    }
    public class ProductRepo : BaseRepository<Product, AfrimartDbContext>, IProductRepo
    {
        private AfrimartDbContext _ctx;
        public ProductRepo(AfrimartDbContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        /// <summary>
        /// Best selling products across all categories
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<Product> GetTrendingProducts(int count)
        {
            return _ctx.Products.Where(x => x.IsDeleted == false).Include(p => p.ProductCategory)
                .Include(x => x.ProductFiles)
                .OrderByDescending(p => p.SalesCount)
                .Take(count).ToList();
        }
        public List<Product> GetBestSellingProductsByCategory(int categoryId, int count)
        {
            return _ctx.Products.Where(p => p.ProductCategoryId == categoryId && p.IsDeleted == false).OrderByDescending(p => p.SalesCount)
                .Take(count).ToList();
        }
        public List<Product> GetNewestProducts(int count)
        {
            return _ctx.Products.Where(x => x.IsDeleted == false).OrderByDescending(p => p.DateCreated)
                .Take(count).ToList();
        }

        public Product GetProductWithCategoryAndFilesByPSIN(string psin)
        {
            return _ctx.Products.Include(x => x.ProductCategory)
                .Include(x => x.ProductFiles).FirstOrDefault(x => x.PSIN.Equals(psin) && x.IsDeleted == false);
        }
        public Product GetProductWithCategoryReviewsAndFilesByPSIN(string psin)
        {
            return _ctx.Products.Include(x => x.ProductCategory)
                .Include(x => x.ProductFiles)
                .Include(p => p.Reviews)
                .FirstOrDefault(x => x.PSIN.Equals(psin) && x.IsDeleted == false);
        }
    }
}
