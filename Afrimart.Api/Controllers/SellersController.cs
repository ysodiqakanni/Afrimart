using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Afrimart.Common;
using Afrimart.DataAccess.DataModels;
using Afrimart.Dto.Stores;
using Afrimart.Service.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace Afrimart.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SellersController : ControllerBase
    {
        private readonly IStoreService _storeService;
        private readonly IAfrimartAuthorizationService _authorizationService;
        private readonly IUserService _userService;

        public SellersController(IStoreService storeService, IAfrimartAuthorizationService authorizationService, IUserService userService)
        {
            _storeService = storeService;
            _authorizationService = authorizationService;
            _userService = userService;
        }
        public async Task<IActionResult> Register()
        {
            return Ok();
        }
         
        [Route("store")]
        public async Task<IActionResult> CreateStore([FromBody] CreateStoreRequestDto request)
        {
            var userEmail = _authorizationService.GetUserEmail();
            var user = await _userService.GetUserByEmail(userEmail);
            
            await _storeService.CreateStore(request, user);

            return Ok("Store created and linked to user");
        }
    }
}
