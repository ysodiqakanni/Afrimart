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
        ShopperProfile GetShopperProfile(string email);
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

        public ShopperProfile GetShopperProfile(string email)
        { 
            var user = _ctx.ShopperProfiles.Include(x => x.User)
                .Single(x => string.Compare(x.User.Email, email, StringComparison.InvariantCultureIgnoreCase) == 0);
            return user;
        }
        public bool GetShopperAddressStatus(string email)
        {
            //var profile = _ctx.ShopperProfiles.Include(x => x.User).First();
            //if (profile.User == null)
            //{
            //    var cust = _ctx.Users.Where(x => x.Id == 1002).Single();
            //    profile.User = cust;

            //    _ctx.Update(profile);
            //    _ctx.SaveChanges();
            //}


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
