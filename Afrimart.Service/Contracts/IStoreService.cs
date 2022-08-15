using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Afrimart.DataAccess.DataModels;
using Afrimart.Dto.Stores;

namespace Afrimart.Service.Contracts
{
    public interface IStoreService
    {
        Task CreateStore(CreateStoreRequestDto request, User user);
        Task<Tuple<bool, string>> AddNewProduct(Product product, string userEmail);
        Task AddProductFiles(List<ProductFile> files, Product productEntity);
        Product GetStoreProductByIdAndEmail(int productId, string storeOwnerEmail);
        ProductListPageDataDto GetProductListData(string storeEmail);
        AddProductPageDataDto GetProductCreationPageData(string storeEmail);
    }
}
