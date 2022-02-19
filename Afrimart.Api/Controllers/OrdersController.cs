using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Afrimart.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        public async Task<IActionResult> CreateOrder()
        {
            return Ok();
        }
        public async Task<IActionResult> CancelOrder()
        {
            return Ok();
        } 
    }
}
