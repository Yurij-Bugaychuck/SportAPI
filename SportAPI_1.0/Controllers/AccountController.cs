using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using SportAPI.Models;
using System.Threading.Tasks;
using SportAPI.Models.User;

namespace SportAPI.Controllers
{
    [ApiController]
    public class Credentials
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AccountController : ControllerBase
    {
        private readonly SportContext _context;

        public AccountController(SportContext context)
        {
            this._context = context;
        }

        [HttpPost("/token")]
        public Task<IActionResult> Token([FromBody] [Bind("Email")] Credentials credentials)
        {
            var identity = this.GetIdentity(credentials.Email);

            if (identity == null)
            {
                return Task.FromResult<IActionResult>(this.BadRequest(new {errorText = "Invalid username or password."}));
            }

            var now = DateTime.UtcNow;

            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(
                    AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                user_id = identity.Name
            };

            return Task.FromResult<IActionResult>(this.Ok(response));
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Create([FromBody] [Bind("Username,Email,FirstName,LastName,Phone")] User user)
        {
            try
            {
                this._context.Add(user);
                await this._context.SaveChangesAsync();

                return this.Ok(user);
            }
            catch (Exception e)
            {
                return this.Conflict(new {errorText = e.Message});
            }
        }

        private ClaimsIdentity GetIdentity(string email)
        {
            User person = this._context.Users.FirstOrDefault(x => x.Email == email);

            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, "user")
                };

                ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(
                        claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);

                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }
    }
}