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
    }
}
