using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Afrimart.DataAccess.DataModels;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Afrimart.DataAccess.Repositories
{ 
    public interface IUserRepo : IBaseRepository<User, AfrimartDbContext>
    {
        void AddUserRole(User user, Role role);
        Task<User> GetSingleUserWithRolesByEmail(string email);
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

        public async Task<User> GetSingleUserWithRolesByEmail(string email)
        { 
            var user = _ctx.Users.Include(x => x.UserRoles).ThenInclude(r => r.Role)
                .SingleOrDefault(x => x.Email.ToLower().Equals(email.ToLower()) && x.IsDeleted == false);

            return user;
        }
    }
}
