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
        public bool IsOnSale { get; set; }
        // may put the below in another class
        public DiscountType DiscountType { get; set; }  // set this when product is added or updated?
        public decimal DiscountValue { get; set; }

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
