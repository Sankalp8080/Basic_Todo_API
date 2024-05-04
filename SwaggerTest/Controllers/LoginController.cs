﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SwaggerTest.Interface;
using SwaggerTest.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                var data = await _user.GetUserList();
                if (data != null)
                {
                    return data;

                }
                else return null;
            }
            catch { throw; }
        }
        [HttpPost("addupdateuser")]
        [Authorize]
        public async Task<int> AddUpdateUser([FromBody] UserIM userIM)
        {
            try
            {
                var data = await _user.AddUpdateUserInfo(userIM);
                if (data == 1)
                {
                    return 1;

                }
                else return 0;
            }
            catch { throw; }
        }
        [HttpPost("getuserbyid")]
        [Authorize]
        public async Task<IEnumerable<UserVM>> GetUserById(int id)
        {
            try
            {
                var data = await _user.GetUserInfo(id);
                if (data != null)
                {
                    return data;

                }
                else return null;
            }
            catch { throw; }
        }

        [HttpPost("deleteuser")]
        [Authorize]
        public async Task<int> DeleteUserbyId(int id)
        {
            try
            {
                var data = await _user.DeleteUserInfo(id);
                if (data == 1)
                {
                    return 1;

                }
                else return 0;
            }
            catch { throw; }
        }

    }


}
