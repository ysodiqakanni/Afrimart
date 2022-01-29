using System;
using System.Collections.Generic;
using System.Text;
using Afrimart.DataAccess.DataModels;
using DataAccess.Repositories;

namespace Afrimart.DataAccess.Repositories
{ 
    public interface IRoleRepo : IBaseRepository<Role, AfrimartDbContext>
    {
    }
    public class RoleRepo : BaseRepository<Role, AfrimartDbContext>, IRoleRepo
    {
        public RoleRepo(AfrimartDbContext ctx) : base(ctx)
        {

        }
    }
}
