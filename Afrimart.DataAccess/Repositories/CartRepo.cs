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
        ShoppingCart GetCart(string cartId);
    }
    public class CartRepo : BaseRepository<ShoppingCart, AfrimartDbContext>, ICartRepo
    {
        private readonly AfrimartDbContext _ctx;
        public CartRepo(AfrimartDbContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        public ShoppingCart GetCart(string cartId)
        {
            var cart = _ctx.Carts.Include("CartItems.Product").SingleOrDefault(x => x.CartIdentifier.Equals(cartId));

            return cart;
        }
    }
}
