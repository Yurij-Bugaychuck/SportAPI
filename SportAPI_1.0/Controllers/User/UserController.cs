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


namespace SportAPI.Controllers
{
    [Route("user")]
    public class UserController : Controller
    {
        private readonly SportContext _context;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly ImageService _ImageService;
        public UserController(SportContext context, IWebHostEnvironment appEnvironment, ImageService imageService)
        {
            _context = context;
            _appEnvironment = appEnvironment;
            _ImageService = imageService;

        }

        // GET: Users List
        [Authorize]
        public async Task<IActionResult> Index()
        {
            User user = _context.Users.FirstOrDefault(o => o.Email == User.Identity.Name);
            return Json(user);
        }



        [Authorize]
        [HttpPost("avatar")]
        public async Task<IActionResult> setAvatar(IFormFile? image)
        {
            
            if (image == null) return StatusCode(404, new { ErrorText = "Avatar supported only in png or jpg" });
            string FileExt = System.IO.Path.GetExtension(image.FileName).ToLower();
            
            List<String> _extensions = new List<string>{ ".png", ".jpg" };
            if (!_extensions.Contains(FileExt))
            {
                
                return StatusCode(413, new { ErrorText = "Avatar supported only in png or jpg" });
            }
            var User = GetUser();
            Debug.WriteLine(_appEnvironment.WebRootPath);
            string StartPath = _appEnvironment.WebRootPath + "/UsersAvatar/" + User.User_id.ToString();
            string ImgPath = await _ImageService.newImage(StartPath, image);
            
            

            //var UserOptionsController = new UserOptionsController(_context);

            //var ImgOption = new User_options{ key = "avatar", value = ImgPath };
            //await UserOptionsController.addStatsByName(new User_options{ key = "avatar", value = ImgPath });


            return Json(ImgPath);
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

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.User_id == id);
            if (user == null)
            {
                return NotFound();
            }

            return Json(user);
        }

       


        //PUT: Edit User
        [HttpPut]
       
        public async Task<IActionResult> Edit([Bind("User_id,Username,Email,First_name,Last_name,Phone")] User user)
        {
            //return Ok("PUT");
            
            try
            {
                var userDB = _context.Users.Where(o=>o.User_id == user.User_id).FirstOrDefault();
                var props = user.GetType().GetProperties();
                foreach(var prop in props)
                {
                    var tmp = prop.GetValue(user, null);
                    prop.SetValue(userDB, tmp);
                }
                _context.Entry(userDB).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.User_id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Json(user);
        }

        // HttpDelete: Delete User
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.User_id == id);
            if (user == null)
            {
                return NotFound();
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Json(user);
        }

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.User_id == id);
        }
        private User GetUser()
        {
            return _context.Users.FirstOrDefault(e => e.Email == User.Identity.Name);
        }
    }

}
