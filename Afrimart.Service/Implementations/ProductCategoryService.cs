using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
        public async Task<ProductCategory> CreateProductCategory(ProductCategory category)
        {
            // check if the category exists - unique by name
            var existing = _uow.ProductCategoryRepo.Find(x => x.Name.ToLower().Equals(category.Name.ToLower()))
                .SingleOrDefault();
            if (existing != null)
            {
                throw new DuplicateNameException("The category already exists");
            }

            await _uow.ProductCategoryRepo.AddAsync(category);
            await _uow.SaveChangesAsync();
            return category;
        }
        public async Task<ProductCategory> UpdateProductCategory(int id, ProductCategory category)
        { 
            var existing = _uow.ProductCategoryRepo.Get(id);
            if (existing == null)
            {
                throw new DuplicateNameException("The category does not exist");
            }

            // now check uniqueness
            var categoryByName = _uow.ProductCategoryRepo.Find(x => x.Name.ToLower().Equals(category.Name.ToLower()))
                .SingleOrDefault();
            if (categoryByName != null && categoryByName.Name.ToLower().Equals(existing.Name.ToLower()) == false)
            {
                throw new DuplicateNameException("The category already exists");
            }

            existing.Name = category.Name;
            existing.ParentId = category.ParentId;
            existing.DisplayImageUri = category.DisplayImageUri;
            
            await _uow.SaveChangesAsync();
            return category;
        }
    }
}
