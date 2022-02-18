using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Afrimart.Dto;

namespace Afrimart.ViewModels.Addresses
{ 

    public class SaveOrUpdateAddressViewModel
    {
        public bool SameAsShipping { get; set; }
        public bool IsEditMode { get; set; }
        public string ReturnUrl { get; set; }

        [Required]
        public AddressViewModel ShippingAddress { get; set; }
        public AddressViewModel BillingAddress { get; set; }
    }

    public class AddressViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string AddressLine1 { get; set; } // Street addr or P.O box
        public string AddressLine2 { get; set; }  // Apt, suite, unit, building, floor etc
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string ZipCode { get; set; }
        public AddressType AddressType { get; set; }
    }
}
