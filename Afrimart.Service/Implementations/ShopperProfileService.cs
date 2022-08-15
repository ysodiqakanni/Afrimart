using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Afrimart.DataAccess;
using Afrimart.DataAccess.DataModels;
using Afrimart.Dto;
using Afrimart.Dto.Addresses;
using Afrimart.Service.Contracts;
using Microsoft.EntityFrameworkCore.Internal;

namespace Afrimart.Service.Implementations
{
    public class ShopperProfileService: IShopperProfileService
    {
        private readonly IUnitOfWork _uow;

        public ShopperProfileService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public bool GetAddressStatus(string userEmail)
        {
            return _uow.ShopperProfileRepo.GetShopperAddressStatus(userEmail); 
        }

        public List<Address> GetUserAddresses(string userEmail)
        {
            return _uow.ShopperProfileRepo.GetShopperAddresses(userEmail);
        }

        public async Task SaveOrUpdateAddresses(string userEmail, SaveOrUpdateAddressDto addressModel)
        {
            var profile = _uow.ShopperProfileRepo.GetShopperProfileIncludeAddresses(userEmail);
            // IEnumerable<Address> addresses = GetUserAddresses(userEmail);
            
            if (profile.Addresses.Any())
            {
                // update
                var shippingAddress = profile.Addresses.First(x => x.AddressType == AddressType.Shipping);
                shippingAddress.AddressLine1 = addressModel.ShippingAddress.AddressLine1;
                shippingAddress.AddressLine2 = addressModel.ShippingAddress.AddressLine2;
                shippingAddress.City = addressModel.ShippingAddress.City;
                shippingAddress.State = addressModel.ShippingAddress.State;
                shippingAddress.ZipCode = addressModel.ShippingAddress.ZipCode;
                shippingAddress.FirstName = addressModel.ShippingAddress.FirstName;
                shippingAddress.LastName = addressModel.ShippingAddress.LastName;

                var billingAddress = profile.Addresses.First(x => x.AddressType == AddressType.Billing);
                billingAddress.AddressLine1 = addressModel.BillingAddress.AddressLine1;
                billingAddress.AddressLine2 = addressModel.BillingAddress.AddressLine2;
                billingAddress.City = addressModel.BillingAddress.City;
                billingAddress.State = addressModel.BillingAddress.State;
                billingAddress.ZipCode = addressModel.BillingAddress.ZipCode;
                billingAddress.FirstName = addressModel.BillingAddress.FirstName;
                billingAddress.LastName = addressModel.BillingAddress.LastName;

                _uow.AddressRepo.Update(shippingAddress);
                _uow.AddressRepo.Update(billingAddress);
            }
            else
            {
                // insert
                var shippingAddress = ConvertDtoToAddress(addressModel.ShippingAddress);
                shippingAddress.ShopperProfile = profile;

                var billingAddress = addressModel.SameAsShipping
                    ? ConvertDtoToAddress(addressModel.ShippingAddress)
                    : ConvertDtoToAddress(addressModel.BillingAddress);
                billingAddress.AddressType = AddressType.Billing;
                billingAddress.ShopperProfile = profile;

                await _uow.AddressRepo.AddAsync(shippingAddress);
                await _uow.AddressRepo.AddAsync(billingAddress);
            }

            await _uow.SaveChangesAsync();
        }

        private Address ConvertDtoToAddress(AddressDto addressDto)
        {
            return new Address()
            {
                AddressLine1 = addressDto.AddressLine1,
                AddressLine2 = addressDto.AddressLine2,
                City = addressDto.City,
                State = addressDto.State,
                ZipCode = addressDto.ZipCode,
                FirstName = addressDto.FirstName,
                LastName = addressDto.LastName,
                Id = addressDto.Id
            };
        }
    }
}
