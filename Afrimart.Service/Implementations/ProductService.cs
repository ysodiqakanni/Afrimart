using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Afrimart.DataAccess;
using Afrimart.DataAccess.DataModels;
using Afrimart.Dto;
using Afrimart.Dto.Public;
using Afrimart.Service.Contracts;

namespace Afrimart.Service.Implementations
{
    public class ProductService: IProductService
    {
        private readonly IUnitOfWork _uow;

        public ProductService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public List<HomeProductCard> GetTrendingProducts(int count)
        {
            return _uow.ProductRepo.GetTrendingProducts(count).Select(x => new HomeProductCard()
            {
                ProductName = x.Name,
                UrlFriendlyProductName = x.Name.Replace(" ", "-"),
                Price = x.SellingPrice,
                Description = x.Description,
                IsOnSale = x.IsOnSale,
                CategoryId = x.ProductCategoryId,
                CategoryName = x.ProductCategory.Name,
                DiscountedPrice = x.DiscountValue,
                ImageUrl = x.DisplayImageUri,
                ProductPSIN = x.PSIN,
                Rating = x.AverageRating,
                ReviewCount = x.ReviewCount,
                UnitsAvailable = x.QuantityAvailable,
                GalleryImgUrls = x.ProductFiles.Where(f => f.FileType == FileType.GalleryImages).Select(f => f.FileUri)
                    .ToList()
            }).ToList();
        }
        public List<HomeProductCard> GetBestSellersByCategory(int categoryId, int count)
        {
            return _uow.ProductRepo.GetBestSellingProductsByCategory(categoryId, count).Select(x => new HomeProductCard()
            {
                ProductName = x.Name,
                UrlFriendlyProductName = x.Name.Replace(" ", "-"),
                Price = x.SellingPrice,
                Description = x.Description,
                IsOnSale = x.IsOnSale,
                CategoryId = x.ProductCategoryId,
                CategoryName = x.ProductCategory.Name,
                DiscountedPrice = x.DiscountValue,
                ImageUrl = x.DisplayImageUri,
                ProductPSIN = x.PSIN,
                Rating = x.AverageRating,
                ReviewCount = x.ReviewCount,
                UnitsAvailable = x.QuantityAvailable,
                GalleryImgUrls = x.ProductFiles.Where(f => f.FileType == FileType.GalleryImages).Select(f => f.FileUri)
                    .ToList()
            }).ToList();
        }

        public HomeProductCard GetProductByPSIN(string psin)
        {
            var product = _uow.ProductRepo.GetProductWihCategoryAndFilesByPSIN(psin);
            return  new HomeProductCard()
            {
                ProductName = product.Name,
                UrlFriendlyProductName = product.Name.Replace(" ", "-"),
                Price = product.SellingPrice,
                Description = product.Description,
                IsOnSale = product.IsOnSale,
                CategoryId = product.ProductCategoryId,
                CategoryName = product.ProductCategory.Name,
                DiscountedPrice = product.DiscountValue,
                ImageUrl = product.DisplayImageUri,
                ProductPSIN = product.PSIN,
                Rating = product.AverageRating,
                ReviewCount = product.ReviewCount,
                UnitsAvailable = product.QuantityAvailable,
                GalleryImgUrls = product.ProductFiles.Where(f => f.FileType == FileType.GalleryImages).Select(f => f.FileUri)
                    .ToList()
            };
        }
    }
}
