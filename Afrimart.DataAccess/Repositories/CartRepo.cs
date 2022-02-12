using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Afrimart.DataAccess.DataModels;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Afrimart.DataAccess.Repositories
{ 
    public interface ICartRepo : IBaseRepository<ShoppingCart, AfrimartDbContext>
    {

    }
    public class CartRepo : BaseRepository<ShoppingCart, AfrimartDbContext>, ICartRepo
    {
        private AfrimartDbContext _ctx;
        public CartRepo(AfrimartDbContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
          
    }
}
