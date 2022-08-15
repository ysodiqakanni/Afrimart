using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Afrimart.ViewModels.Sellers
{
    public class SellerProductListViewModel
    {
        public List<SellerProductCardViewModel> Products { get; set; }
        public DashboardHeaderViewModel DashboardHeaderViewModel { get; set; } = new DashboardHeaderViewModel();
    }

    public class SellerProductCardViewModel
    {
        public string Name { get; set; }
        public string ImageUri { get; set; }
        public decimal Price { get; set; }
        public int TotalSales { get; set; }
        public decimal TotalEarnings { get; set; }
    }
}
