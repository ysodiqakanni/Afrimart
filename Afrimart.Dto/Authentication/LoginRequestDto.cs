using System;
using System.ComponentModel.DataAnnotations;

namespace Afrimart.Dto.Authentication
{
    public class LoginRequestDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
