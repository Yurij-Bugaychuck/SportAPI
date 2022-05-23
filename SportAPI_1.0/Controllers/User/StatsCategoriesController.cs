using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportAPI.Models;
using SportAPI.Interfaces;
using SportAPI.Models.User;

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
            this._context = context;
            this._categoriesService = categoriesService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return this.Ok(this._categoriesService.GetStatsCategories());
        }

        // GET: StatsCategories/Details/5
        [HttpGet("{categoryId}")]
        public async Task<IActionResult> Details(Guid categoryId)
        {
            return this.Ok(this._categoriesService.GetStatsCategoryById(categoryId));
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name")] StatsCategory statsCategories)
        {
            if (this.ModelState.IsValid)
            {
                this._categoriesService.AddStatsCategory(statsCategories);
            }

            return this.Ok(statsCategories);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name")] StatsCategory statsCategories)
        {
            var CatDb = this._categoriesService.GetStatsCategoryById(id);

            if (CatDb == null)
                return this.NotFound();

            CatDb.Name = statsCategories.Name;

            this._categoriesService.UpdateStatsCategory(CatDb);

            return this.Ok(CatDb);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            this._categoriesService.RemoveStatsCategory(id);

            return this.Ok("deleted");
        }
    }
}