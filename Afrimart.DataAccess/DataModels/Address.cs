using System;
using System.Collections.Generic;
using System.Text;
using Afrimart.Dto;
using DataModels;

namespace Afrimart.DataAccess.DataModels
{
    public class Address:Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AddressLine1 { get; set; } // Street addr or P.O box
        public string AddressLine2 { get; set; }  // Apt, suite, unit, building, floor etc
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public string Phone { get; set; }

        public AddressType AddressType{get; set; }

        public int ShopperProfileId { get; set; }
        public ShopperProfile ShopperProfile { get; set; }
    }

    //public class ShippingAddress : Address
    //{

    //}
    //public class BillingAddress : Address
    //{

    //}

   
}
