using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using SportAPI.Models;
using Microsoft.AspNetCore.Hosting;

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
            this._hostingEnvironment = hostingEnvironment;
            this._context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (this._context.Users.ToList().Count > 0)
            {
                return this.Ok(this._context.Users.ToList());
            }
            else
            {   
                return this.NotFound("Not found");
            }
        }


    }
}
