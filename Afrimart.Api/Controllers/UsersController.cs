using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Afrimart.DataAccess.DataModels;
using Afrimart.Dto.Users;
using Afrimart.Service.Contracts;

namespace Afrimart.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] CreateUserRequestDto request)
        {

            if (_userService.UserExists(request.Email))
            {
                return BadRequest("The email is taken");
            }

            var user = new User()
            {
                Email = request.Email
            };
            await _userService.CreateUser(user, request.Password);
            // find if the user exists
            return Ok();
        }
    }
}
