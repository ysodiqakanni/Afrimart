using System;
using System.Collections.Generic;
using System.Text;
using Afrimart.DataAccess.DataModels;
using DataAccess.Repositories;

namespace Afrimart.DataAccess.Repositories
{ 
    public interface IUserRepo : IBaseRepository<User, AfrimartDbContext>
    {
    }
    public class UserRepo : BaseRepository<User, AfrimartDbContext>, IUserRepo
    {
        public UserRepo(AfrimartDbContext ctx) : base(ctx)
        {
            
        }
    }
}
