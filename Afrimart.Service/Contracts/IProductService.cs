using System;
using System.Collections.Generic;
using System.Text;
using Afrimart.DataAccess.DataModels;
using Afrimart.Dto.Public;

namespace Afrimart.Service.Contracts
{
    public interface IProductService
    {
        List<HomeProductCard> GetTrendingProducts(int count);
        List<HomeProductCard> GetBestSellersByCategory(int categoryId, int count);
        HomeProductCard GetProductByPSIN(string psin);
        Product GetProductEntityWithImagesAndReviewsByPSIN(string psin);
    }
}
