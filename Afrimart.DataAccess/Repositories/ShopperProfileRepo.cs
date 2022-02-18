using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Afrimart.DataAccess.DataModels;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Afrimart.DataAccess.Repositories
{ 
    public interface IShopperProfileRepo : IBaseRepository<ShopperProfile, AfrimartDbContext>
    {
        ShopperProfile GetShopperProfileIncludeAddresses(string email);
        bool GetShopperAddressStatus(string email);
        List<Address> GetShopperAddresses(string email);
    }
    public class ShopperProfileRepo : BaseRepository<ShopperProfile, AfrimartDbContext>, IShopperProfileRepo
    {
        private AfrimartDbContext _ctx;
        public ShopperProfileRepo(AfrimartDbContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        public ShopperProfile GetShopperProfileIncludeAddresses(string email)
        { 
            var user = _ctx.ShopperProfiles.Include(x => x.User)
                .Include(x => x.Addresses)
                .Single(x => x.User.Email.ToLower().Equals(email.ToLower()));
            return user;
        }
        public bool GetShopperAddressStatus(string email)
        { 
            var profileWithAddress = _ctx.ShopperProfiles.Include(x => x.User)
                .Include(x => x.Addresses)
                .SingleOrDefault(x => x.User.Email.ToLower().Equals(email.ToLower())
                && x.Addresses.Any());
            return profileWithAddress != null;
        }
        public List<Address> GetShopperAddresses(string email)
        {
            var profileAddresses = _ctx.ShopperProfiles.Include(x => x.User)
                .Include(x => x.Addresses)
                .Single(x => x.User.Email.ToLower().Equals(email.ToLower())).Addresses;
            return profileAddresses;
        }
    }
}
