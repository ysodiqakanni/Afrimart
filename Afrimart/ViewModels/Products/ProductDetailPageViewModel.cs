using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Afrimart.Dto.Public;
using Afrimart.ViewModels.Home;

namespace Afrimart.ViewModels.Products
{
    public class ProductDetailPageViewModel: HomeProductCard
    { 
        public List<ReviewCardViewModel>Reviews { get; set; }
        public List<HomeProductCard> RelatedProducts { get; set; }
    }
}
