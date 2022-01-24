using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Afrimart.DataAccess.DataModels;
using DataAccess.Repositories;

namespace Afrimart.DataAccess.Repositories
{
    public interface IProductCategoryRepo: IBaseRepository<ProductCategory, AfrimartDbContext>
    {
    }
    public class ProductCategoryRepo : BaseRepository<ProductCategory, AfrimartDbContext>, IProductCategoryRepo
    {
        public ProductCategoryRepo(AfrimartDbContext ctx): base(ctx)
        {
            
        } 
    }
}
