using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportAPI.Models;
using SportAPI.Interfaces;

namespace SportAPI.Controllers
{
    [Route("api/user/stats/categories")]
    [ApiController]
    public class StatsCategoriesController : ControllerBase
    {
        private readonly SportContext _context;
        private readonly ICategoriesService _categoriesService;

        public StatsCategoriesController(SportContext context, ICategoriesService categoriesService)
        {
            _context = context;
            _categoriesService = categoriesService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(_categoriesService.GetStatsCategories());
        }

        // GET: StatsCategories/Details/5
        [HttpGet("{categoryId}")]
        public async Task<IActionResult> Details(Guid categoryId)
        {
            return Ok(_categoriesService.GetStatsCategoryById(categoryId));
        }
        
        [HttpPost]
        
        public async Task<IActionResult> Create([Bind("Name")] StatsCategory statsCategories)
        {
            if (ModelState.IsValid)
            {
                _categoriesService.AddStatsCategory(statsCategories);
            }
            return Ok(statsCategories);
        }       

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name")] StatsCategory statsCategories)
        {

            var CatDb = _categoriesService.GetStatsCategoryById(id);

            if (CatDb == null) return NotFound();

            CatDb.Name = statsCategories.Name;

            _categoriesService.UpdateStatsCategory(CatDb);

            return Ok(CatDb);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            _categoriesService.RemoveStatsCategory(id);

            return Ok("deleted");
        }
    }
}
