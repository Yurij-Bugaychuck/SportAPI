using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportAPI.Models;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
namespace SportAPI.Controllers
{
    [Authorize]
    [Route("user/stats")]
    public class UserStatsController : Controller
    {
        private readonly SportContext _context;
        private readonly User _user;

        public UserStatsController(SportContext context)
        {
            _context = context;

        }
        [HttpGet]
        public async Task<IActionResult> get()
        {
            User user = _context.Users.FirstOrDefault(o => o.Email == User.Identity.Name);
            var UserStats = (from o in _context.Users_stats
                            where o.User_id == user.User_id
                            orderby o.Created_at descending 
                            select (new { key = o.key, value = o.value, Created_at = o.Created_at }))
                            .AsEnumerable().GroupBy(o=>o.key).ToDictionary(key => key.Key, value => value.Select(o => o.value).FirstOrDefault());
                  

            return Json(UserStats);
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
            _context.SaveChanges();

            return Ok(stats);
        }
    }
}
