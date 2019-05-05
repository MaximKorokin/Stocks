using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Stocks.Services;
using Stocks.Entities;
using Stocks.Models;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User user)
        {
            var authUser = _usersService.Authenticate(user.Name, user.Password);

            if (authUser == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(authUser);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("register")]
        public IActionResult Register([FromBody]User user)
        {
            if (_usersService.Register(user))
                return Ok();
            return BadRequest();
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _usersService.GetAll();
            return Ok(users);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _usersService.GetById(id);

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

        [HttpPost("edit")]
        public IActionResult Edit(User user)
        {
            user.Id = int.Parse(User.Identity.Name);
            if (_usersService.EditUser(user))
                return Ok();
            return BadRequest();
        }
    }
}
