using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Afrimart.DataAccess.DataModels;

namespace Afrimart.Service.Contracts
{
    public interface IProductCategoryService
    {
        Task<ProductCategory> CreateNewProject(ProductCategory category);
    }
}
