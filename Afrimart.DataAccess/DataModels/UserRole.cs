using System;
using System.Collections.Generic;
using System.Text;
using DataModels;

namespace Afrimart.DataAccess.DataModels
{
    public class UserRole: Entity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
