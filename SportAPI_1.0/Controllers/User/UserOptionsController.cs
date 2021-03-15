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
            var UserOptions = (from o in _context.UsersOptions
                             where o.UserId == user.UserId
                             orderby o.CreatedAt descending
                             select (new { key = o.Key, value = o.Value, Created_at = o.CreatedAt }))
                            .AsEnumerable().GroupBy(o => o.key).ToDictionary(key => key.Key, value => value.Select(o => o.value).FirstOrDefault());


            return Json(UserOptions);
        }
        

        [HttpGet("{key}")]
        public async Task<IActionResult> getStatsByKey(string? key)
        {
            User user = _context.Users.FirstOrDefault(o => o.Email == User.Identity.Name);
            var UsersStats = await _context.UsersStats.Where(o => o.UserId == user.UserId && o.Key == key).OrderBy(o=>o.CreatedAt).ToListAsync();

            return Json(UsersStats);
        }

        [HttpPost]
        public async Task<IActionResult> addStatsByName([Bind("key,value")] UserOption option )
        {
            User user = _context.Users.FirstOrDefault(o => o.Email == User.Identity.Name);
            option.UserId = user.UserId;
            _context.UsersOptions.Add(option);

            return Json(option);
        }
    }
}
