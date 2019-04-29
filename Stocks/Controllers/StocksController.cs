using Microsoft.AspNetCore.Mvc;
using Stocks.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stocks.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StocksController : Controller
    {
        private IUsersService _userService;

        public StocksController(IUsersService userService)
        {
            _userService = userService;
        }

        [HttpGet("[action]")]
        public IActionResult A()
        {
            return Ok("12313");
        }
    }
}
