using System;
using System.Collections.Generic;
using System.Text;
using DataModels;

namespace Afrimart.DataAccess.DataModels
{
    public class ShopperProfile:Entity
    {
        public string PhoneNumber { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public List<Address> Addresses { get; set; }

        // list of orders
    }
}
