﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Afrimart.DataAccess.DataModels;

namespace Afrimart.Service.Contracts
{
    public interface IUserService
    {
        Task<User> GetUserByEmailAndPassword(string email, string password);
    }
}