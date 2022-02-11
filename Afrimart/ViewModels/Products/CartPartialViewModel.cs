using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Afrimart.ViewModels.Products
{
    public class CartPartialViewModel
    {
        public decimal CartAmount { get; set; }
        public List<CartItemDto> CartItems { get; set; }
    }

    public class CartItemDto
    {
        public string ProductName { get; set; }
        public string ProductImageUri { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string ProductPSIN { get; set; }
    }
}
