using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportAPI.Models;
using SportAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using SportAPI.Models.User;

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
            this._context = context;
            this._userService = userService;
        }



 
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            User user = this._userService.GetByEmail(this.User.Identity.Name);
            var userOptions = this._userService.GetUserOptions(user);

            return this.Ok(userOptions);
        }
        

        [HttpGet("{key}")]
        public async Task<IActionResult> GetOptionByKey(string key)
        {
            User user = this._userService.GetByEmail(this.User.Identity.Name);
            var UsersStats = this._userService.GetUserOptionByKey(user, key);

            return this.Ok(UsersStats);
        }

        [HttpPost]
        public async Task<IActionResult> AddOption([Bind("Key,Value")] UserOption option )
        {
            //return Ok(option);
            if (option.Key == null) throw new ArgumentNullException();
            User user = this._userService.GetByEmail(this.User.Identity.Name);

            option = await this._userService.AddUserOption(user, option);

            return this.Ok(option);
        }

        [HttpPut("{optionId}")]
        public async Task<IActionResult> UpdateOption(Guid optionId, [Bind("key,value")] UserOption option)
        {
           
            if (option.Key == null) throw new ArgumentNullException();
            User user = this._userService.GetByEmail(this.User.Identity.Name);

            option.UserOptionsId = optionId;


            option = await this._userService.UpdateUserOption(user, option);

            return this.Ok(option);
        }

        [HttpDelete("{optionId}")]
        public async Task<IActionResult> RemoveOption(Guid optionId)
        {
            User user = this._userService.GetByEmail(this.User.Identity.Name);

            var option = await this._userService.RemoveUserOption(user, optionId);

            return this.Ok("deleted");
        }


    }
}