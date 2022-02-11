using System;
using System.Collections.Generic;
using System.Text;
using Afrimart.Dto.Public;

namespace Afrimart.Dto.Products
{
    public class ProductDetailPageResponseDto : HomeProductCard
    {
        public decimal SellingPrice { get; set; } // could be the normal price or the Discounted price
        public int FiveStarRatingCount { get; set; }
        public int FourStarRatingCount { get; set; }
        public int ThreeStarRatingCount { get; set; }
        public int TwoStarRatingCount { get; set; }
        public int OneStarRatingCount { get; set; }
        public List<ReviewCardResponseDto> Reviews { get; set; }
        public List<HomeProductCard> RelatedProducts { get; set; }
    }
    public class ReviewCardResponseDto
    {
        public string CustomerName { get; set; }
        public string CustomerImageUri { get; set; }
        public string Review { get; set; }
        public string Pros { get; set; }
        public string Cons { get; set; }
        public int Rating { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
