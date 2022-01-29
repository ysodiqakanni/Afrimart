using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DataModels;

namespace Afrimart.DataAccess.DataModels
{
    public class Role: Entity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public Guid GuidId { get; set; } = Guid.NewGuid();

        public virtual ICollection<UserRole> UserRoles { get; } = new List<UserRole>();
    }
}
