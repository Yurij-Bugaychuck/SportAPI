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
    [Route("user")]
    public class UserController : Controller
    {
        private readonly SportContext _context;

        public UserController(SportContext context)
        {
            _context = context;
           

        }

        // GET: Users List
        [Authorize]
        public async Task<IActionResult> Index()
        {
            User user = _context.Users.FirstOrDefault(o => o.Email == User.Identity.Name);
            return Ok(user);
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

            return Ok(user);
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
            return Ok(user);
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

            return Ok(user);
        }

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.User_id == id);
        }
    }

}
