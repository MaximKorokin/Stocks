using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Stocks.Data;
using Stocks.Entities;
using Stocks.Helpers;
using Stocks.Models;

namespace Stocks.Services
{
    public interface IUsersService
    {
        TokenUser Authenticate(string username, string password);
        bool Register(User user);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }

    public class UsersService : IUsersService
    {
        private readonly AppSettings _appSettings;
        private readonly StocksDbContext _db;

        public UsersService(IOptions<AppSettings> appSettings, StocksDbContext db)
        {
            _appSettings = appSettings.Value;
            _db = db;
        }

        public TokenUser Authenticate(string username, string password)
        {
            var authUser = _db.Users.SingleOrDefault(x => x.Name == username && x.Password == password);

            // return null if user not found
            if (authUser == null)
                return null;

            TokenUser user = new TokenUser { Id = authUser.Id, Name = authUser.Name, Password = authUser.Password, Role = authUser.Role };


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

        public bool Register(User user)
        {
            if (_db.Users.FirstOrDefault(u => u.Name == user.Name) != null ||
                user.Password == "")
                return false;
            _db.Users.Add(user);
            _db.SaveChanges();
            return true;
        }

        public IEnumerable<User> GetAll()
        {
            // return users without passwords
            return _db.Users
                .AsEnumerable()
                .Select(x => {
                x.Password = null;
                return x;
            });
        }

        public User GetById(int id)
        {
            var user = _db.Users.FirstOrDefault(x => x.Id == id);

            // return user without password
            if (user != null)
                user.Password = null;

            return user;
        }
    }
}
