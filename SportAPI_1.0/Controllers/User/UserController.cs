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
        public async Task<IActionResult> Index()
        {
            User user = _userService.GetByEmail(User.Identity.Name);
            return Ok(user);
        }
        
        [HttpPost]
        public async Task<IActionResult> Index([Bind("Username,Email,FirstName,LastName,Phone")] User user)
        {
            Guid userId = await _userService.CreateUser(user);
            return Ok(userId);
        }


        //POST: Write new Avatar Image to User Options
        [Authorize]
        [HttpPost("avatar")]
        public async Task<IActionResult> setAvatar(IFormFile? image)
        {
            if (image == null) return StatusCode(404);

            string FileExt = System.IO.Path.GetExtension(image.FileName).ToLower();
            List<String> _extensions = new List<string>{ ".png", ".jpg" };
            if (!_extensions.Contains(FileExt))
            { 
                return StatusCode(413, new { ErrorText = "Avatar supported only in png or jpg" });
            }

            var User = _userService.GetByEmail(HttpContext.User.Identity.Name);
            
            string StartPath = Path.Combine(_appEnvironment.WebRootPath, "UsersAvatar", User.UserId.ToString());
            string ImgPath = await _ImageService.newImage(StartPath, image);

            var ImgOption = new UserOption{ Key = "avatar", Value = ImgPath };

            var res = _userService.AddUserOption(User, ImgOption);
            


            return Ok(ImgPath);
        }

        // GET: Get User By Id
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> getuser(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _userService.GetById((Guid)id);
            {
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
