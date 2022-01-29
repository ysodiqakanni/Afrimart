using System;
using System.Collections.Generic;
using System.Text;
using Afrimart.DataAccess.DataModels;
using DataAccess.Repositories;

namespace Afrimart.DataAccess.Repositories
{ 
    public interface IStoreRepo : IBaseRepository<Store, AfrimartDbContext>
    {
    }
    public class StoreRepo : BaseRepository<Store, AfrimartDbContext>, IStoreRepo
    {
        public StoreRepo(AfrimartDbContext ctx) : base(ctx)
        {

        }
    }
}
