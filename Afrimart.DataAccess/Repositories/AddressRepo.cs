using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Afrimart.DataAccess.DataModels;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Afrimart.DataAccess.Repositories
{ 
    public interface IAddressRepo : IBaseRepository<Address, AfrimartDbContext>
    {
         
    }
    public class AddressRepo : BaseRepository<Address, AfrimartDbContext>, IAddressRepo
    {
        private AfrimartDbContext _ctx;
        public AddressRepo(AfrimartDbContext ctx) : base(ctx)
        {
            _ctx = ctx;
        } 
    }
}
