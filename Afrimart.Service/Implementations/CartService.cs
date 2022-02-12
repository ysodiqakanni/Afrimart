using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Afrimart.DataAccess;
using Afrimart.DataAccess.DataModels;
using Afrimart.Service.Contracts;

namespace Afrimart.Service.Implementations
{
    public class CartService : ICartService
    {
        private IUnitOfWork _uow;
        public CartService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task AddToCart(string cartId, string psin, int count)
        {
            var product = _uow.ProductRepo.Find(x => x.PSIN.Equals(psin) && x.IsDeleted == false).Single();
            var cart = _uow.CartRepo.Find(c => c.CartIdentifier.Equals(cartId)).SingleOrDefault();
            if (cart == null)
            {
                // create a new cart
                await _uow.CartRepo.AddAsync(new ShoppingCart()
                {
                    CartIdentifier = cartId, 
                    CartItems = new List<CartItem>()
                    {
                        new CartItem()
                        {
                            Quantity = count,
                            Product = product,
                            NetAmount = product.SellingPrice * count
                        }
                    }
                });
                //await _uow.CartItemRepo.AddAsync(new CartItem()
                //{
                //    Quantity = count,
                //    Product = product,
                //    NetAmount = product.SellingPrice * count,
                //    ShoppingCart = cart
                //});
            }
            else
            {
                // update existing cart
                var cartItem = cart.CartItems.SingleOrDefault(x => x.ProductId == product.Id);
                if (cartItem != null)
                {
                    cartItem.Quantity += count;
                    cartItem.NetAmount = cartItem.Quantity * product.SellingPrice;
                    
                }
                else
                {
                    cart.CartItems.Add(new CartItem()
                    {
                        Quantity = count,
                        Product = product,
                        NetAmount = product.SellingPrice * count
                    });
                }
            }

            await _uow.SaveChangesAsync();
        }

        public ShoppingCart GetCart(string cartIdentifier)
        {
            return _uow.CartRepo.FindInclude(x => x.CartIdentifier.Equals(cartIdentifier), new List<string>(){"CartItems" })
                .FirstOrDefault();
        }
    }
}
