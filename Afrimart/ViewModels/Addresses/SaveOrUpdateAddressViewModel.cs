using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Afrimart.Dto;
using Afrimart.Dto.Addresses;

namespace Afrimart.ViewModels.Addresses
{ 

    public class SaveOrUpdateAddressViewModel
    {
        public bool SameAsShipping { get; set; }
        public bool IsEditMode { get; set; }
        public string ReturnUrl { get; set; }

        [Required]
        public AddressDto ShippingAddress { get; set; }
        public AddressDto BillingAddress { get; set; }
    }

    public class AddressViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; } // Street addr or P.O box

        [Display(Name = "Address Line 2")]
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
