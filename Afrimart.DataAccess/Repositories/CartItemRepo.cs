using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Afrimart.DataAccess.DataModels;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Afrimart.DataAccess.Repositories
{ 
    public interface ICartItemRepo : IBaseRepository<CartItem, AfrimartDbContext>
    {

    }
    public class CartItemRepo : BaseRepository<CartItem, AfrimartDbContext>, ICartItemRepo
    {
        private AfrimartDbContext _ctx;
        public CartItemRepo(AfrimartDbContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
          
    }
}
