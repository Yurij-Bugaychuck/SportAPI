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
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace SportAPI.Controllers
{
    [Route("/")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly SportContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public HomeController(SportContext context, IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (_context.Users.ToList().Count > 0)
            {
                return Ok( _context.Users.ToList());
            }
            else
            {   
                return NotFound("Not found");
            }
        }


    }
}
