using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Afrimart.DataAccess.DataModels;
using Afrimart.Dto.Public;

namespace Afrimart.Service.Contracts
{
    public interface IProductCategoryService
    {
        Task<ProductCategory> CreateProductCategory(ProductCategory category);
        Task<ProductCategory> UpdateProductCategory(int id, ProductCategory category);
        ProductCategory GetSpecialCategoryForHomePage();
    }
}
