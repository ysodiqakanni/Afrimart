using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DataModels;

namespace Afrimart.DataAccess.DataModels
{
    public class User: Entity
    { 
        [Required]
        public Guid GuidId { get; set; } = Guid.NewGuid();

        [Required]
        public string Email { get; set; }
        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }

        public bool ConfirmEmail { get; set; } 
        public string PhoneNumber { get; set; }  
        public string LastName { get; set; } 
        public string FirstName { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; } = new List<UserRole>();
    }
}
