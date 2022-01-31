using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Afrimart.ViewModels.Sellers
{
    public class AddProductViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsOnSale { get; set; }
        public decimal SalePrice { get; set; }
        public decimal DisplayPrice { get; set; } // To be computed for display only

        public string Tags { get; set; }


        public DashboardHeaderViewModel DashboardHeaderViewModel { get; set; } = new DashboardHeaderViewModel();
    }
}
