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
            var UserStats = (from o in _context.UsersStats
                            where o.UserId == user.UserId
                            orderby o.CreatedAt descending 
                            select (new { key = o.Key, value = o.Value, Created_at = o.CreatedAt }))
                            .AsEnumerable().GroupBy(o=>o.key).ToDictionary(key => key.Key, value => value.Select(o => o.value).FirstOrDefault());
                  

            return Json(UserStats);
        }
        
     
        [HttpGet("{key}")]
        public async Task<IActionResult> getStatsByKey(string? key)
        {
            User user = _context.Users.FirstOrDefault(o => o.Email == User.Identity.Name);
            var UsersStats = await _context.UsersStats.Where(o => o.UserId == user.UserId && o.Key == key).ToListAsync();

            return Ok(UsersStats);
        }

        [HttpPost]
        public async Task<IActionResult> addStatsByName([Bind("key,value")] UserStat stats )
        {
            User user = _context.Users.FirstOrDefault(o => o.Email == User.Identity.Name);
            stats.UserId = user.UserId;
            _context.UsersStats.Add(stats);
            _context.SaveChanges();

            return Ok(stats);
        }
    }
}
