using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Afrimart.Dto.Orders
{
    public class CreateOrderRequestDto
    {
        public string CartId { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal Tax { get; set; }
        public ShippingMethod ShippingMethod { get; set; }

        public decimal TotalCost { get; set; }

        // payment card
        [Required(ErrorMessage = "Card details are required")] 
       public CardDetailsDto CardDetails { get; set; }
        public bool AcceptTermsAndConditions { get; set; } 
    }

    public class CardDetailsDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Number { get; set; }
        [Required]
        public string Expiry { get; set; }
        [Required]
        public string Cvc { get; set; }

        public bool SaveCardDetails { get; set; }
    }
}
