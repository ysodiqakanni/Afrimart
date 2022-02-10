using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using DataModels;

namespace Afrimart.DataAccess.DataModels
{
    public class Product: Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// Platform Standard Identification Number
        /// A standard and unique 10 digit identifier for each product
        /// </summary>
        public string PSIN { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal UnitPrice { get; set; } // will be price per weight for weightable products

        /// <summary>
        /// This is the price that the item is sold for at any time, discounted or not
        /// So, selling price = unitPrice when no sale, and = unit-discount when on sale
        /// selling price must already include PLATFORM FEE (certain percentage out of selling price) and tax
        /// </summary>
        [Column(TypeName = "decimal(5, 2)")]
        public decimal SellingPrice { get; set; } // The DisplayPrice

        public bool IsWeighted { get; set; }
        public string MeasurementUnit { get; set; }  // For weighted products only

        [Column(TypeName = "decimal(5, 2)")]
        public decimal Tax { get; set; }
        public string Tags { get; set; }
        public int QuantityAvailable { get; set; }

        public bool IsOnSale { get; set; }
        // we can keep the % as float and display as int
        [Column(TypeName = "decimal(5, 2)")]
        public decimal DiscountValue { get; set; }
        public double DiscountPercentage { get; set; } // save discount value of percentage?
       public string DisplayImageUri { get; set; }

       [Column(TypeName = "decimal(5, 2)")]
       public decimal TotalEarnings { get; set; }
       public int SalesCount { get; set; }
       public int ReviewCount { get; set; }
       public double AverageRating { get; set; }

        public int ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }

        public int StoreId { get; set; }
        public Store Store { get; set; }

        public List<ProductFile> ProductFiles { get; set; }

        public virtual List<ProductReview> Reviews { get; set; }

        // ******* CASES ********** //
        // do we have different product forms
    }

    public class WeightableProduct: Product
    {
        public string WeightUnit { get; set; } // could be lb, gms, kg etc 

       
    }
    public class CountableProduct : Product
    {
        
    }

    public class SaleDiscount
    {
        // discount type: % or fixed
    }

    public enum DiscountType
    {
        Percentage,
        Fixed
    }

    // for product forms. a product has many forms
}
