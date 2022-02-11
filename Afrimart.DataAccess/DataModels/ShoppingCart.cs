using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModels;

namespace Afrimart.DataAccess.DataModels
{
    public class ShoppingCart:Entity
    {
        public string CartIdentifier { get; set; }

        public decimal TotalAmount
        {
            get
            {
                return CartItems.Sum(x => x.NetAmount);
            }
            set => TotalAmount = value;
        }
        public List<CartItems> CartItems { get; set; }
    }

    public class CartItems
    {
        public int Quantity { get; set; }
        public decimal NetAmount { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
         
        public ShoppingCart ShoppingCart { get; set; }
    }
}
