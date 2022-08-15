using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Afrimart.DataAccess.DataModels;

namespace Afrimart.Service.Contracts
{
    public interface ICartService
    {
        Task AddToCart(string cartId, string psin, int count);
        ShoppingCart GetCart(string cartIdentifier);
        Task RemoveProductFromCart(string cartId, string psin);
    }
}
