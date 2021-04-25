using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
using SportAPI.Interfaces;


namespace SportAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly ImageService _ImageService;
        private readonly IUserService _userService;
        public UserController(IWebHostEnvironment appEnvironment, ImageService imageService, IUserService userService)
        {
            _appEnvironment = appEnvironment;
            _ImageService = imageService;
            _userService = userService;
        }

        // GET: Users List
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            User user = _userService.GetByEmail(User.Identity.Name);
            return Ok(user);
        }


        //POST: Write new Avatar Image to User Options
        [Authorize]
        [HttpPost("avatar")]
        public async Task<IActionResult> setAvatar(IFormFile image)
        {
            if (image == null) return StatusCode(404);
            image = image as IFormFile;

            User user = _userService.GetByEmail(User.Identity.Name);
            string FileExt = System.IO.Path.GetExtension(image.FileName).ToLower();
            List<String> _extensions = new List<string> { ".png", ".jpg", ".jpeg" };
            if (!_extensions.Contains(FileExt))
            {
                return StatusCode(413, new { ErrorText = "Avatar supported only in png or jpg" });
            }

            var imgOption = await _userService.NewAvatar(user, image);


            return Ok(imgOption);
        }

        [Authorize]
        [HttpGet("avatar")]
        public async Task<IActionResult> GetAvatar()
        {
            User user = _userService.GetByEmail(User.Identity.Name);
            var avatar = _userService.GetAvatar(user);

            return Ok(avatar);
        }

        [Authorize]
        [HttpGet("about")]
        public async Task<IActionResult> GetAbout()
        {
            User user = _userService.GetByEmail(User.Identity.Name);
            var about = _userService.GetAbout(user);

            return Ok(about);
        }

        [Authorize]
        [HttpPost("about")]
        public async Task<IActionResult> PostAbout([FromBody] string aboutValue)
        {
            Debug.WriteLine("KEEEEEEEEEK----> " + aboutValue);
            User user = _userService.GetByEmail(User.Identity.Name);
            var about = _userService.AddAbout(user, aboutValue);

            return Ok(about);
        }

        [Authorize]
        [HttpGet("avatar/list")]
        public async Task<IActionResult> GetAvatarList()
        {
            User user = _userService.GetByEmail(User.Identity.Name);
            var avatar = _userService.GetAvatars(user);

            return Ok(avatar);
        }

        [Authorize]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById( Guid userId)
        {

            var user = _userService.GetById(userId);
            if (user == null){
                return NotFound();
            }

            return Ok(user);
        }

       


        //PUT: Edit User
        [HttpPut]
       
        public async Task<IActionResult> Edit([Bind("FirstName,LastName,Phone")] User user)
        {
            User userDB = _userService.GetByEmail(User.Identity.Name);

            if (userDB == null) return NotFound();

            var res = await _userService.UpdateUser(userDB, user);
         
            return Ok(res);
        } 
    }

}
