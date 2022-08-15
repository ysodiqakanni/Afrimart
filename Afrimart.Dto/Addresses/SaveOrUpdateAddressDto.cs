using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Afrimart.Dto.Addresses
{
    public class SaveOrUpdateAddressDto
    {
        public bool SameAsShipping { get; set; }

        [Required]
        public AddressDto ShippingAddress { get; set; }
        public AddressDto BillingAddress { get; set; }
    }
    public class AddressDto
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string ZipCode { get; set; }

        public AddressType AddressType { get; set; }
    }
}
