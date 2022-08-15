using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        [Column(TypeName = "decimal(6, 2)")]
        public decimal TotalEarnings { get; set; } // earnings across all products
        public int TotalSalesCount { get; set; }
        public int TotalReviewCount { get; set; }  // Across all products
        public double AverageRating { get; set; }
        public string StoreLogoUri { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}
