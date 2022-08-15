using System;
using System.Collections.Generic;
using System.Text;

namespace Afrimart.Dto.Addresses
{
    public class AddressListResponseDto
    {
        //public bool SameAsShipping { get; set; } 
        public AddressDto ShippingAddress { get; set; }
        public AddressDto BillingAddress { get; set; }
    }
}
