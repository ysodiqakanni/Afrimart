using System;
using System.Collections.Generic;
using System.Text;

namespace Afrimart.Dto
{
    public static class AfrimartConstants
    {
        public static string SELLER_ROLE = "Seller";
        public static string ADMIN_ROLE = "Admin";
        public static string SUPER_ADMIN_ROLE = "SuperAdmin";

        public static string CART_ID_SESSION_KEY = "Afr_CartIdn";

        public static int MAXIMUM_PER_PRODUCT_UNITS_PER_CART = 20;
        public static int PRODUCT_TRANSACTION_FEE_PERCENTAGE = 4;

        public static decimal MINIMUM_STANDARD_SHIPPING_COST = 10;
        public static decimal TEXAS_TAX_PERCENTAGE = 0.05m;
    }
}
