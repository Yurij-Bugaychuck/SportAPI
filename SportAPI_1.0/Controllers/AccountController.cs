using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using SportAPI.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SportAPI.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SportContext _context;


        public AccountController(SportContext context)
        {
            _context = context;
        }

        [HttpPost("/token")]
        public async Task<IActionResult> Token(string email)
        {
            var identity = GetIdentity(email);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                user_id = identity.Name
            };

            return Ok(response);
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Create([Bind("Username,Email,First_name,Last_name,Phone")] User user)
        {
            try
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return Ok(user);
            }
            catch(Exception e)
            {
                return Conflict(new { errorText = e.Message });
            }
           
        }

        private ClaimsIdentity GetIdentity(string email)
        {
            User person = _context.Users.FirstOrDefault(x => x.Email == email);
            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, "user")
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }
    }
}
