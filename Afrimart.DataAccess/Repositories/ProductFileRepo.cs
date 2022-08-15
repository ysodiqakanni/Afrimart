using System;
using System.Collections.Generic;
using System.Text;
using Afrimart.DataAccess.DataModels;
using DataAccess.Repositories;

namespace Afrimart.DataAccess.Repositories
{ 
    public interface IProductFileRepo : IBaseRepository<ProductFile, AfrimartDbContext>
    {
    }
    public class ProductFileRepo : BaseRepository<ProductFile, AfrimartDbContext>, IProductFileRepo
    {
        public ProductFileRepo(AfrimartDbContext ctx) : base(ctx)
        {

        }
    }
}
