using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Afrimart.Dto;
using Afrimart.ViewModels.Products;

namespace Afrimart.ViewModels.Orders
{
    public class OrderCheckoutViewModel
    {
        // Todo: In the api, use this cartId to pull the items and do the calculations
        // Also verify the costs
        public string CartId { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal Tax { get; set; }
        public ShippingMethod ShippingMethod { get; set; }

        // payment card
        [Required]
        public string Name { get; set; }
        [Required]
        public string Number { get; set; }
        [Required]
        public string Expiry { get; set; }
        [Required]
        public string Cvc { get; set; }

        public bool SaveCardDetails { get; set; }
        public bool AcceptTermsAndConditions { get; set; }
        // Display only
        public string AddressInfo { get; set; }
        public CartPartialViewModel CartSummary { get; set; }
    }

    //public enum ShippingMethod
    //{
    //    StandardShipping,
    //    LocalPickup
    //}
}
