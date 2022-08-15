using System;
using System.Collections.Generic;
using System.Text;

namespace Afrimart.Dto.Stores
{
    public class ProductListPageDataDto
    {
        public DashboardHeaderResponseDto DashboardData { get; set; }
        public List<SellerProductCardDto> Products { get; set; }
    }
}
