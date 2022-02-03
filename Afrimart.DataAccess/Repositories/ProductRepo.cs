using System;
using System.Collections.Generic;
using System.Text;
using Afrimart.DataAccess.DataModels;
using DataAccess.Repositories;

namespace Afrimart.DataAccess.Repositories
{ 
    public interface IProductRepo : IBaseRepository<Product, AfrimartDbContext>
    {
    }
    public class ProductRepo : BaseRepository<Product, AfrimartDbContext>, IProductRepo
    {
        public ProductRepo(AfrimartDbContext ctx) : base(ctx)
        {

        }
    }
}
