﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Afrimart.DataAccess;
using Afrimart.DataAccess.DataModels;
using Afrimart.Service.Contracts;

namespace Afrimart.Service.Implementations
{
    public class AddressService: IAddressService
    {
        private readonly IUnitOfWork _uow;

        public AddressService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public bool GetAddressStatus(string userEmail)
        {
            var exist = _uow.AddressRepo.Find(x => x.ShopperProfile.User.Email.ToLower().Equals(userEmail.ToLower()));
            throw new NotImplementedException();
        }

        public List<Address> GetUserAddresses(string userEmail)
        {
            throw new NotImplementedException();
        }

        public async Task SaveOrUpdateAddresses(string userEmail)
        {
            throw new NotImplementedException();
        }
    }
}
