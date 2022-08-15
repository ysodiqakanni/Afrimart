using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Afrimart.DataAccess;
using Afrimart.DataAccess.DataModels;
using Afrimart.Dto;
using Afrimart.Dto.Stores;
using Afrimart.Service.Contracts;

namespace Afrimart.Service.Implementations
{
    public class StoreService: IStoreService
    {
        private readonly IUnitOfWork _uow;

        public StoreService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task CreateStore(CreateStoreRequestDto request, User user)
        {
            // check that the user doesn't have one already
            var existingStore = _uow.StoreRepo.Find(x => x.UserId == user.Id).SingleOrDefault();
            if (existingStore != null)
            {
                // Todo: log error
                throw new InvalidOperationException("A store already exists for this user");
            }

            var sellerRole = _uow.RoleRepo.Find(x => x.Name.Equals(AfrimartConstants.SELLER_ROLE)).Single();
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.PhoneNumber = request.PhoneNumber;
            
             
            var store = new Store()
            {
                StoreName = request.StoreName,
                AddressLine = request.AddressLine,
                City = request.City,
                Zip = request.Zip,
                EIN = request.EIN,
                User = user
            };

            _uow.UserRepo.Update(user);
            await _uow.StoreRepo.AddAsync(store);
            // add seller role to user
            _uow.UserRepo.AddUserRole(user, sellerRole);

            await _uow.SaveChangesAsync();
        }

        public async Task<Tuple<bool, string>> AddNewProduct(Product product, string userEmail)
        {
            var store = _uow.StoreRepo.Find(x => x.User.Email.ToLower().Equals(userEmail)).SingleOrDefault();
            if (store == null)
            {
                // Todo: send a log to support
                return new Tuple<bool, string>(false, "Unable to retrieve store data");
            }

            product.PSIN = GenerateNew10DigitProductCode();
            product.Store = store;
            var savedProduct = await _uow.ProductRepo.AddAsync(product);
            await _uow.SaveChangesAsync();

            // Todo: check that savedProduct brings an ID
            return new Tuple<bool, string>(true, savedProduct.Id.ToString());
        }

        public async Task AddProductFiles(List<ProductFile> files, Product productEntity)
        {
            await _uow.ProductFileRepo.AddRangeAsync(files);

            var displayImageUrl = files.FirstOrDefault(f => f.FileType == FileType.DisplayImage)?.FileUri;
            if (!String.IsNullOrEmpty(displayImageUrl))
            {
                productEntity.DisplayImageUri = displayImageUrl;
                _uow.ProductRepo.Update(productEntity);
            }
            await _uow.SaveChangesAsync();
        }

        public Product GetStoreProductByIdAndEmail(int productId, string storeOwnerEmail)
        {
            return _uow.StoreRepo.GetProductByIdAndStoreOwnerEmail(productId, storeOwnerEmail);
        }

        public ProductListPageDataDto GetProductListData(string storeEmail)
        {
            var store = _uow.StoreRepo.GetStoreWithProductsByOwnerEmail(storeEmail);
            var result = new ProductListPageDataDto()
            {
                DashboardData = new DashboardHeaderResponseDto()
                {
                    StoreName = store.StoreName,
                    LogoUri = store.StoreLogoUri,
                    RatingCount = store.TotalReviewCount,
                    TotalSales = store.TotalSalesCount,
                    AverageRating = store.AverageRating,
                    MemberSince = $"{store.DateCreated.Month.ToString()} {store.DateCreated.Year}"
                },
                Products = store.Products.Select(p => new SellerProductCardDto()
                {
                    Name = p.Name,
                    Price = p.SellingPrice,
                    TotalSales = p.SalesCount,
                    TotalEarnings = p.TotalEarnings,
                    ImageUri = p.DisplayImageUri
                }).ToList()
            };

            return result;
        }
        public AddProductPageDataDto GetProductCreationPageData(string storeEmail)
        {
            var prodCategories = _uow.ProductCategoryRepo.GetAll().OrderBy(x => x.Name).ToList();

            var store = _uow.StoreRepo.GetStoreByOwnerEmailNoInclude(storeEmail);
            var result = new AddProductPageDataDto()
            {
                DashboardData = new DashboardHeaderResponseDto()
                {
                    StoreName = store.StoreName,
                    LogoUri = store.StoreLogoUri,
                    RatingCount = store.TotalReviewCount,
                    TotalSales = store.TotalSalesCount,
                    AverageRating = store.AverageRating,
                    MemberSince = $"{store.DateCreated.Month.ToString()} {store.DateCreated.Year}"
                },
                Categories = prodCategories.Select(x => new ProductCategoriesDto()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList()
            };

            return result;
        }

        // TOdo: move to ProductService or something -> A common function perhaps
        public string GenerateNew10DigitProductCode()
        {
            int length = 10;
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
