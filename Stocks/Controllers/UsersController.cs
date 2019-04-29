using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Stocks.Services;
using Stocks.Entities;
using Stocks.Models;

namespace WebApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private IUsersService _userService;

        public UsersController(IUsersService userService)
        {
            _userService = userService;
        }

        [HttpGet("[action]")]
        public IActionResult A()
        {
            return Ok("ahahahha");
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User user)
        {
            var authUser = _userService.Authenticate(user.Name, user.Password);

            if (authUser == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(authUser);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("register")]
        public IActionResult Register([FromBody]User user)
        {
            if (_userService.Register(user))
                return Ok();
            return BadRequest();
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            // only allow admins to access other user records
            var currentUserId = int.Parse(User.Identity.Name);
            if (id != currentUserId && !User.IsInRole(Role.Admin))
            {
                return Forbid();
            }

            return Ok(user);
        }
    }
}
