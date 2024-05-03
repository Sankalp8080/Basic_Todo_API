using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SwaggerTest.Interface;
using SwaggerTest.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SwaggerTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _configuration;
        private IUser _user;
        public LoginController(IConfiguration configuration, IUser user)
        {
            _configuration = configuration;
            _user = user;
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserIM user)
        {
            IActionResult response = Unauthorized();
            var userDetail = AuthenticationUser(user);
            if (userDetail != null)
            {
                var tokenString = GenerateToken(userDetail);
                response = Ok(new { token = tokenString });
            }
            return response;
        }
        [NonAction]
        private UserIM AuthenticationUser(UserIM user)
        {
            UserIM u = null;
            if (user.username.ToLower() == "admin")
            {
                u = new UserIM { username = user.username, password = user.password };
            }
            return u;
        }
        [NonAction]
        public string GenerateToken(UserIM user)
        {
            var sKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var cc = new SigningCredentials(sKey, SecurityAlgorithms.HmacSha256);
            var claim = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.username)

            };
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claim,
                expires: DateTime.Now.AddDays(3),
                signingCredentials: cc
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        [HttpGet("getuserlist")]
        [Authorize]
        public async Task<List<UserVM>> GetUserList()
        {
            try
            {
                return await _user.GetUserList();
            }
            catch { throw; }
        }
        [HttpPost("addupdateuser")]
        [AllowAnonymous]
        public async Task<int> AddUpdateUser([FromBody] UserIM userIM)
        {
            try
            {
              return await  _user.AddUpdateUserInfo(userIM);
               
            }catch { throw; }
        }

    }


}
