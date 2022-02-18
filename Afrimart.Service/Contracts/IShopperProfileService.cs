using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Afrimart.DataAccess.DataModels;
using Afrimart.Dto.Addresses;

namespace Afrimart.Service.Contracts
{
    public interface IShopperProfileService
    {
        bool GetAddressStatus(string userEmail);
        List<Address> GetUserAddresses(string userEmail); 
        Task SaveOrUpdateAddresses(string userEmail, SaveOrUpdateAddressDto addressModel);
    }
}
