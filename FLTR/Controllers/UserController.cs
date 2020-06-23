using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FLTR.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FLTR.Controllers
{
    [ApiController]
    [Route("Users")]
    public class UserController
    {
        private IUserService _userService; 
        
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpPost]
        [Route("CreateUser")]
        public object CreateUser(string email, string passwordHash)
        {
            var mySecret = "asdv234234^&%&^%&^hjsdfb2%%%";
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));

            var myIssuer = "http://fltr.roman-bondarenko.azurewebsites.com";
            var myAudience = $"http://fltr.roman-bondarenko.azurewebsites.com/{email}";

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("guid", _userService.CreateUser(email, passwordHash).ToString()),
                    new Claim("email", email.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = myIssuer,
                Audience = myAudience,
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        [HttpPost]
        [Route("LogIn")]
        public string LogIn(string email, string passwordHash)
        {
            return Guid.NewGuid().ToString();
        }
        
        [HttpPost]
        [Route("LogOut")]
        public string LogOut(string email, string passwordHash)
        {
            return "Ok";
        }

        [HttpPost]
        [Route("DeleteUser")]
        public string DeleteUser(string email, string passwordHash)
        {
            return "Ok";
        }
    }
}