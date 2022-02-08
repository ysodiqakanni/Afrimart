using System;
using System.Collections.Generic;
using System.Text;

namespace Afrimart.Dto.Public
{
    // Index view model
    // get trending products -> Fetch top 8 products order by sales count desc
    // imgUrl, prodName, categName, catId, price, rating, 
    // for quick view: galleryImgUrls, IsOnSale, salesPrice, prodCount, description
    // 3 most popular categories to showcase (logic for this??
    // categoryImg, name, 
    // 1 special category: (maybe Ankara for now) logic??
    // pull top 12 products in the category

    public class HomepageDataResponseDto
    {
        public List<HomeProductCard> TrendingProducts { get; set; }
        public List<PopularCategoryCard> PopularCategories { get; set; }

        public PopularCategoryCard SpecialCategory { get; set; }
        public List<HomeProductCard> SpecialCategoryProducts { get; set; }
    }

    public class HomeProductCard
    {
        public string ImageUrl { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ProductPSIN { get; set; }
        public decimal Price { get; set; }
        public double Rating { get; set; }

        // Quick view
        public List<string> GalleryImgUrls { get; set; }
        public bool IsOnSale { get; set; }
        public decimal DiscountedPrice { get; set; }
        public double UnitsAvailable { get; set; }
        public string Description { get; set; }
    }

    public class PopularCategoryCard
    {
        public string CategoryName { get; set; }
        public string CategoryImgUri { get; set; }
        public int CategoryId { get; set; }
    }
}
