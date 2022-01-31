using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Afrimart.ViewModels.Sellers
{
    public class DashboardHeaderViewModel
    {
        public string StoreName { get; set; }
        public string LogoUri { get; set; }
        public string MemberSince { get; set; }
        public int TotalSales { get; set; }
        public int RatingCount { get; set; }
        public double AverageRating { get; set; }
    }
}
