using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportAPI.Models;
using SportAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
namespace SportAPI.Controllers
{
    [Route("api/user/stats")]
    [ApiController]
    [Authorize]
    public class UserStatsController : ControllerBase
    {
        private readonly SportContext _context;
        private readonly IUserService _userService;

        public UserStatsController(SportContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            User user = _userService.GetByEmail(User.Identity.Name);
            var UserStats = _userService.GetUserStats(user);
                  
            return Ok(UserStats);
        }
        
        [HttpGet("{key}")]
        public async Task<IActionResult> GetStatsByKey(string? key)
        {
            User user = _userService.GetByEmail(User.Identity.Name);
            var UsersStats = _userService.GetUserStatByKey(user, key);

            return Ok(UsersStats);
        }


        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetStatsByCategory(Guid categoryId)
        {
            User user = _userService.GetByEmail(User.Identity.Name);
            var UsersStats = _userService.GetUserStatByCategory(user, categoryId);

            return Ok(UsersStats);
        }





        [HttpPost]
        public async Task<IActionResult> AddStatsByName([Bind("Key,Value,CreatedAt,StatsCategoryId")] UserStat stat)
        {
            User user = _userService.GetByEmail(User.Identity.Name);
            if (stat.Key == "" || stat.Key.Length < 1) throw new ArgumentException();

            var stats = await _userService.AddUserStat(user, stat);

            return Ok(stats);
        }

        [HttpPut("{statId}")]
        public async Task<IActionResult> UpdateOption(Guid statId, [Bind("key,value,StatsCategoryId")] UserStat stat)
        {
            if (stat.Key == null) throw new ArgumentNullException();
            User user = _userService.GetByEmail(User.Identity.Name);

            stat.UserStatsId = statId;


            stat = await _userService.UpdateUserStat(user, stat);

            return Ok(stat);
        }

        [HttpDelete("{statId}")]
        public async Task<IActionResult> RemoveOption(Guid statId)
        {
            User user = _userService.GetByEmail(User.Identity.Name);

            var stat = await _userService.RemoveUserStat(user, statId);

            return Ok("deleted");
        }
    }
}
