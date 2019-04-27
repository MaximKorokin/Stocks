using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Stocks.Entities;
using Stocks.Helpers;

namespace Stocks.Services
{
    public interface IUsersService
    {
        TokenUser Authenticate(string username, string password);
        IEnumerable<TokenUser> GetAll();
        TokenUser GetById(int id);
    }

    public class UserService : IUsersService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<TokenUser> _users = new List<TokenUser>
        {
            new TokenUser { Id = 1, Name = "admin", Password = "admin", Role = Role.Admin },
            new TokenUser { Id = 2, Name = "user", Password = "user", Role = Role.User }
        };

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public TokenUser Authenticate(string username, string password)
        {
            var user = _users.SingleOrDefault(x => x.Name == username && x.Password == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Password = null;

            return user;
        }

        public IEnumerable<TokenUser> GetAll()
        {
            // return users without passwords
            return _users.Select(x => {
                x.Password = null;
                return x;
            });
        }

        public TokenUser GetById(int id)
        {
            var user = _users.FirstOrDefault(x => x.Id == id);

            // return user without password
            if (user != null)
                user.Password = null;

            return user;
        }
    }
}
