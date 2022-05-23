﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportAPI.Models;
using SportAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using SportAPI.Models.User;

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
            this._context = context;
            this._userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            User user = this._userService.GetByEmail(this.User.Identity.Name);
            var UserStats = this._userService.GetUserStats(user);
                  
            return this.Ok(UserStats);
        }
        
        [HttpGet("{key}")]
        public async Task<IActionResult> GetStatsByKey(string? key)
        {
            User user = this._userService.GetByEmail(this.User.Identity.Name);
            var UsersStats = this._userService.GetUserStatByKey(user, key);

            return this.Ok(UsersStats);
        }


        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetStatsByCategory(Guid categoryId)
        {
            User user = this._userService.GetByEmail(this.User.Identity.Name);
            var UsersStats = this._userService.GetUserStatByCategory(user, categoryId);

            return this.Ok(UsersStats);
        }





        [HttpPost]
        public async Task<IActionResult> AddStatsByName([Bind("Key,Value,CreatedAt,StatsCategoryId")] UserStat stat)
        {
            User user = this._userService.GetByEmail(this.User.Identity.Name);
            if (stat.Key == "" || stat.Key.Length < 1) throw new ArgumentException();

            var stats = await this._userService.AddUserStat(user, stat);

            return this.Ok(stats);
        }

        [HttpPut("{statId}")]
        public async Task<IActionResult> UpdateOption(Guid statId, [Bind("key,value,StatsCategoryId")] UserStat stat)
        {
            if (stat.Key == null) throw new ArgumentNullException();
            User user = this._userService.GetByEmail(this.User.Identity.Name);

            stat.UserStatsId = statId;


            stat = await this._userService.UpdateUserStat(user, stat);

            return this.Ok(stat);
        }

        [HttpDelete("{statId}")]
        public async Task<IActionResult> RemoveOption(Guid statId)
        {
            User user = this._userService.GetByEmail(this.User.Identity.Name);

            var stat = await this._userService.RemoveUserStat(user, statId);

            return this.Ok("deleted");
        }
    }
}
