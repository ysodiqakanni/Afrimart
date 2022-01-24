using System;
using System.Collections.Generic;
using System.Text;

namespace Afrimart.Dto.Authentication
{
    public class LoginResponseDto
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        // to save user obj in session and get it for display purpose
        public LoginUserDto User { get; set; }
     
    }
    public class LoginUserDto
    {
        public string Email { get; set; }
        public string FullName { get; set; } 
    }
}
