using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportAPI.Models;

namespace SportAPI.Controllers
{
    [Route("api/user/stats/categories")]
    [ApiController]
    public class StatsCategoriesController : ControllerBase
    {
        private readonly SportContext _context;

        public StatsCategoriesController(SportContext context)
        {
            _context = context;
        }

        // GET: StatsCategories
        public async Task<IActionResult> Index()
        {
            
            return Ok(await _context.StatsCategories.ToListAsync());
        }

        // GET: StatsCategories/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statsCategories = await _context.StatsCategories
                .FirstOrDefaultAsync(m => m.StatsCategoryId == id);
            if (statsCategories == null)
            {
                return NotFound();
            }

            return Ok(statsCategories);
        }
        // POST: StatsCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StatsCategoryId,Name,CreatedAt")] StatsCategories statsCategories)
        {
            if (ModelState.IsValid)
            {
                statsCategories.StatsCategoryId = Guid.NewGuid();
                _context.Add(statsCategories);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return Ok(statsCategories);
        }

        // GET: StatsCategories/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statsCategories = await _context.StatsCategories.FindAsync(id);
            if (statsCategories == null)
            {
                return NotFound();
            }
            return Ok(statsCategories);
        }

        // POST: StatsCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name")] StatsCategories statsCategories)
        {
            if (id != statsCategories.StatsCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(statsCategories);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatsCategoriesExists(statsCategories.StatsCategoryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return Ok(statsCategories);
        }

        // GET: StatsCategories/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statsCategories = await _context.StatsCategories
                .FirstOrDefaultAsync(m => m.StatsCategoryId == id);
            if (statsCategories == null)
            {
                return NotFound();
            }

            return Ok(statsCategories);
        }

        // POST: StatsCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var statsCategories = await _context.StatsCategories.FindAsync(id);
            _context.StatsCategories.Remove(statsCategories);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StatsCategoriesExists(Guid id)
        {
            return _context.StatsCategories.Any(e => e.StatsCategoryId == id);
        }
    }
}
