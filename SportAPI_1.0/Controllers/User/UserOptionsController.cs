using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportAPI.Models;
using Microsoft.AspNetCore.Authorization;
namespace SportAPI.Controllers
{
    [Authorize]
    [Route("user/options")]
    public class UserOptionsController : Controller
    {
        private readonly SportContext _context;

        public UserOptionsController(SportContext context)
        {
            _context = context;

        }

 
        [HttpGet]
        public async Task<IActionResult> get()
        {
            User user = _context.Users.FirstOrDefault(o => o.Email == User.Identity.Name);
            var UsersOptions = from o in _context.Users_options where o.User_id == user.User_id orderby o.Created_at group o by o.key;
            

            return Ok(UsersOptions);
        }
        

        [HttpGet("{key}")]
        public async Task<IActionResult> getStatsByKey(string? key)
        {
            User user = _context.Users.FirstOrDefault(o => o.Email == User.Identity.Name);
            var UsersStats = await _context.Users_stats.Where(o => o.User_id == user.User_id && o.key == key).ToListAsync();

            return Ok(UsersStats);
        }

        [HttpPost]
        public async Task<IActionResult> addStatsByName([Bind("key,value")] User_stats stats )
        {
            User user = _context.Users.FirstOrDefault(o => o.Email == User.Identity.Name);
            stats.User_id = user.User_id;
            _context.Users_stats.Add(stats);

            return Ok(stats);
        }
    }
}
