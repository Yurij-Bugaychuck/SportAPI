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

namespace SportAPI.Controllers
{
    [Route("api/user/options")]
    [ApiController]
    [Authorize]
    public class UserOptionsController : ControllerBase
    {
        private readonly SportContext _context;
        private readonly IUserService _userService;

        public UserOptionsController(SportContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }



 
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            User user = _userService.GetByEmail(User.Identity.Name);

            var userOptions = _userService.GetUserOptions(user);

            return Ok(userOptions);
        }
        

        [HttpGet("{key}")]
        public async Task<IActionResult> GetOptionByKey(string key)
        {
            User user = _userService.GetByEmail(User.Identity.Name);
            var UsersStats = _userService.GetUserOptionByKey(user, key);

            return Ok(UsersStats);
        }

        [HttpPost]
        public async Task<IActionResult> AddOption([Bind("key,value")] UserOption option )
        {
            User user = _userService.GetByEmail(User.Identity.Name);

            option = await _userService.AddUserOption(user, option);

            return Ok(option);
        }


    }
}
