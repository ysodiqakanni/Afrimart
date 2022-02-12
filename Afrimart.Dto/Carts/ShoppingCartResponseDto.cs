using System;
using System.Collections.Generic;
using System.Text;

namespace Afrimart.Dto.Carts
{ 
    public class ShoppingCartResponseDto
    {
        public decimal CartAmount { get; set; }
        public List<CartItemDto> CartItems { get; set; } = new List<CartItemDto>();
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
