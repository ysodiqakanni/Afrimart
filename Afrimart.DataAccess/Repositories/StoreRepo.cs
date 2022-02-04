using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Afrimart.DataAccess.DataModels;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Afrimart.DataAccess.Repositories
{ 
    public interface IStoreRepo : IBaseRepository<Store, AfrimartDbContext>
    {
        Product GetProductByIdAndStoreOwnerEmail(int productId, string storeOwnerEmail);
    }
    public class StoreRepo : BaseRepository<Store, AfrimartDbContext>, IStoreRepo
    {
        private AfrimartDbContext _ctx;
        public StoreRepo(AfrimartDbContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
        public Product GetProductByIdAndStoreOwnerEmail(int productId, string storeOwnerEmail)
        {
            var store = _ctx.Stores.Include(s => s.User)
                .Include(s => s.Products)
                .SingleOrDefault(s => s.User.Email.ToLower().Equals(storeOwnerEmail.ToLower()));
            if (store == null) return null;

            var theProduct = store.Products.SingleOrDefault(p => p.Id == productId);

            return theProduct;
        }
    }
}
