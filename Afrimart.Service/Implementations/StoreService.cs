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
    }
}
