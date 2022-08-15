using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Afrimart.DataAccess.DataModels;
using Afrimart.Dto.Addresses;

namespace Afrimart.Service.Contracts
{
    public interface IAddressService
    {
        AddressDto ConvertToDto(Address address);
    }
}
