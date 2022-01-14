using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Auth.Api.Models;
using Auth.Common;
using Auth.Common.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Auth.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IOptions<AuthOptions> _authOptions;
        private readonly Repository _repository;

        public AuthController(IOptions<AuthOptions> authOptions, Repository repository)
        {
            _authOptions = authOptions;
            _repository = repository;
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] Login request)
        {
            var user = AuthenticateUser(request.Email, request.Password).Result;

            if (user != null)
            {
                var token = GenerateJwt(user);

                return Ok(new { access_token = token });
            }

            return Unauthorized();
        }

        private async Task<User> AuthenticateUser(string requestEmail, string requestPassword)
        {
            var users = await _repository.GetUserAsync();
            return users.FirstOrDefault(u => u.Email == requestEmail && u.Password == requestPassword);
        }

        private string GenerateJwt(User user)
        {
            var authParams = _authOptions.Value;

            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim("role",user.Role )
            };

            var token = new JwtSecurityToken(
                authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.LiftTime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
