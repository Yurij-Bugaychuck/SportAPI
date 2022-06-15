using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
using System.Linq;
using SportAPI.Interfaces;
using SportAPI.Models.User;
using SportAPI.Services;

namespace SportAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IWebHostEnvironment AppEnvironment { get; }
        private ImageService ImageService { get; }
        private IUserService UserService { get; }
        
        public UserController(IWebHostEnvironment appEnvironment, ImageService imageService, IUserService userService)
        {
            this.AppEnvironment = appEnvironment;
            this.ImageService = imageService;
            this.UserService = userService;
        }
        
        [Authorize]
        [HttpGet]
        public Task<IActionResult> Get()
        {
            User user = this.UserService.GetByEmail(this.User.Identity?.Name);

            Models.OutModel.User responseUser = new()
            {
                UserId = user.UserId,
                About = this.UserService.GetAbout(user).Value,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Images = this.UserService.GetAvatars(user).Select(avatar => avatar.Value),
                Phone = user.Phone,
                Username = user.Username
            };
            
            return Task.FromResult<IActionResult>(this.Ok(responseUser));
        }
        
        [Authorize]
        [HttpPost("avatar")]
        public async Task<IActionResult> SetAvatar(IFormFile image)
        {
            if (image == null) 
                return this.StatusCode(404);

            User user = this.UserService.GetByEmail(this.User.Identity.Name);
            
            string fileExt = System.IO.Path.GetExtension(image.FileName).ToLower();
            
            List<String> extensions = new List<string> {".png", ".jpg", ".jpeg"};
            
            if (!extensions.Contains(fileExt))
            {
                return this.StatusCode(413, new {ErrorText = "Avatar supported only in png or jpg"});
            }

            var imgOption = await this.UserService.NewAvatar(user, image);
            
            return this.Ok(imgOption);
        }

        [Authorize]
        [HttpGet("avatar")]
        public async Task<IActionResult> GetAvatar()
        {
            User user = this.UserService.GetByEmail(this.User.Identity?.Name);
            var avatar = this.UserService.GetAvatar(user);

            return this.Ok(avatar);
        }

        [Authorize]
        [HttpGet("about")]
        public async Task<IActionResult> GetAbout()
        {
            User user = this.UserService.GetByEmail(this.User.Identity?.Name);
            var about = this.UserService.GetAbout(user);

            return this.Ok(about);
        }

        [Authorize]
        [HttpPost("about")]
        public async Task<IActionResult> PostAbout([FromBody] string aboutValue)
        {
            Debug.WriteLine("KEEEEEEEEEK----> " + aboutValue);
            User user = this.UserService.GetByEmail(this.User.Identity?.Name);
            var about = this.UserService.UpdateAbout(user, aboutValue);

            return this.Ok(about);
        }

        [Authorize]
        [HttpGet("avatar/list")]
        public async Task<IActionResult> GetAvatarList()
        {
            User user = this.UserService.GetByEmail(this.User.Identity.Name);
            var avatar = this.UserService.GetAvatars(user);

            return this.Ok(avatar);
        }

        [Authorize]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            var user = this.UserService.GetById(userId);
            
            if (user == null){
                return this.NotFound();
            }

            return this.Ok(user);
        }
        
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Edit([Bind("FirstName,LastName,Phone")] User user)
        {
            User authUser = this.UserService.GetByEmail(this.User.Identity?.Name);

            if (authUser == null) return this.NotFound();

            var res = await this.UserService.UpdateUser(authUser, user);
         
            return this.Ok(res);
        } 
    }

}
