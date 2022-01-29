using System;
using System.Collections.Generic;
using System.Text;
using Afrimart.DataAccess.DataModels;
using DataAccess.Repositories;

namespace Afrimart.DataAccess.Repositories
{ 
    public interface IUserRepo : IBaseRepository<User, AfrimartDbContext>
    {
        void AddUserRole(User user, Role role);
    }
    public class UserRepo : BaseRepository<User, AfrimartDbContext>, IUserRepo
    {
        private AfrimartDbContext _ctx;
        public UserRepo(AfrimartDbContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        public void AddUserRole(User user, Role role)
        {
            _ctx.Add(new UserRole()
            {
                User = user,
                Role = role
            });
        }
    }
}
