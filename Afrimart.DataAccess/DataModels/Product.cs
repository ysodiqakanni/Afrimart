using System;
using System.Collections.Generic;
using System.Text;
using DataModels;

namespace Afrimart.DataAccess.DataModels
{
    public class Product: Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; } // will be price per weight for weightable products

        /// <summary>
        /// This is the price that the item is sold for at any time, discounted or not
        /// So, selling price = unitPrice when no sale, and = unit-discount when on sale
        /// selling price must already include PLATFORM FEE (certain percentage out of selling price)
        /// </summary>
        public decimal SellingPrice { get; set; }

        // WHEN ON SALE
        // display the original price (base plus fee)
        // display the discounted price ((base - discount) plus fee)



        // WHEN NO SALE
        // display the original price (base price plus fee)
        



        public bool IsOnSale { get; set; }
        // we can keep the % as float and display as int
        public double DiscountPercentage { get; set; } // save discount value of percentage?
        // may put the below in another class
        // public DiscountType DiscountType { get; set; }  // set this when product is added or updated?


        public ProductCategory Category { get; set; }


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
