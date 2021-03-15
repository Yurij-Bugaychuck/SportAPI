using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportAPI.Models;
using Microsoft.EntityFrameworkCore;

using System.Text.Json;
using System.Text.Json.Serialization;

using System;

namespace SportAPI.Controllers
{   

    [Route("/")]
    public class HomeController : Controller
    {
        private readonly SportContext _context;

        public HomeController(SportContext context)
        {
            
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (_context.Users.ToList().Count > 0)
            {
                return Json( _context.Users.Include(o => o.Workouts).ToList());
            }
            else
            {   
                return Ok("Not found");
            }
        }
        
        [HttpGet("view")]
        public async Task<IActionResult> View()
        {
            return Ok(await _context.Messages.ToListAsync());
        }

        
        [HttpGet("add")]
        public void add()
        {
            Message msg = new Message { Value = "Hello, World!" };
            _context.Messages.Add(msg);
            _context.SaveChanges();

            HttpContext.Response.Redirect("/view");
           
        }
        
        [HttpGet("delete/{id}")]
        public void delete(int? id)
        {
            if (id != null) HttpContext.Response.Redirect("/");
            
            Message msg = _context.Messages.FirstOrDefault(p=>p.Id == id);
            if (msg != null)
            {
                _context.Messages.Remove(msg);
                _context.SaveChanges();
            }
           

            HttpContext.Response.Redirect("/view");
           
        }
    }
}
