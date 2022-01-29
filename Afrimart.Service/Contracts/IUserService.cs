using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Afrimart.DataAccess.DataModels;

namespace Afrimart.Service.Contracts
{
    public interface IUserService
    {
        Task<User> GetUserByEmailAndPassword(string email, string password);
        bool UserExists(string email);
        Task<User> CreateUser(User user, string password);
        Task<User> GetUserByEmail(string email);
    }
}
