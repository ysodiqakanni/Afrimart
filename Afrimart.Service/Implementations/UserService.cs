using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Afrimart.DataAccess;
using Afrimart.DataAccess.DataModels;
using Afrimart.Service.Contracts;

namespace Afrimart.Service.Implementations
{
    public class UserService: IUserService
    {
        private readonly IUnitOfWork _uow;
        public UserService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<User> GetUserByEmailAndPassword(string email, string password)
        {
            return _uow.UserRepo.Find(x => String.Compare(email, x.Email, StringComparison.CurrentCultureIgnoreCase) == 0).FirstOrDefault();
        }
    }
}
