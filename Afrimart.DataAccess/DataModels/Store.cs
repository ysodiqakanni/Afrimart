using System;
using System.Collections.Generic;
using System.Text;
using DataModels;

namespace Afrimart.DataAccess.DataModels
{ 
    public class Store: Entity
    { 
        public string StoreName { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string EIN { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        // public virtual List<Product> Products { get; set; }
    }
}
