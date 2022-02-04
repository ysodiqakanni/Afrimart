using System;
using System.Collections.Generic;
using System.Text;

namespace Afrimart.Dto.Stores
{
    public class AddProductPageDataDto
    {
        public DashboardHeaderResponseDto DashboardData { get; set; }
        public List<ProductCategoriesDto> Categories { get; set; }
    }
}
