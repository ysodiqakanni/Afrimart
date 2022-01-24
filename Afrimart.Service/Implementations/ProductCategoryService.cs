using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Afrimart.DataAccess;
using Afrimart.DataAccess.DataModels;
using Afrimart.DataAccess.Repositories;
using Afrimart.Service.Contracts;

namespace Afrimart.Service.Implementations
{
    public class ProductCategoryService: IProductCategoryService
    { 
        private readonly IUnitOfWork _uow;

        public ProductCategoryService(IUnitOfWork uow)
        { 
            _uow = uow;
        }
        public async Task<ProductCategory> CreateNewProject(ProductCategory category)
        {
            // check if the category exists
            await _uow.ProductCategoryRepo.AddAsync(category);
            await _uow.SaveChangesAsync();
            return category;
        }
    }
}
