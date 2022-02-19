using System;
using System.Collections.Generic;
using System.Text;
using Afrimart.Dto.Addresses;

namespace Afrimart.Dto.Carts
{
    public class CheckoutPageDataDto
    {
        public AddressListResponseDto AddressData { get; set; }
        public ShoppingCartResponseDto CartSummary { get; set; }
    }
}
