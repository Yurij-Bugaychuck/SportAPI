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
    [Authorize]
    [Route("workout/{workoutId}/option")]
    public class WorkoutOptionsController : Controller
    {
        private readonly SportContext _context;

        public WorkoutOptionsController(SportContext context)
        {
            _context = context;
        }

 
        [HttpGet("/list")]
        public async Task<IActionResult> get(Guid? workoutId)
        {
            if (workoutId == null) return NotFound();
           

            var workoutOptions = (from o in _context.WorkoutsOptions
                                  where o.WorkoutId == workoutId
                                  orderby o.CreatedAt descending
                                  select (new { key = o.Key, value = o.Value, Created_at = o.CreatedAt })).AsEnumerable().ToDictionary(o => o.key);


            return Json(workoutOptions);
        }
        

        [HttpGet("{key}")]
        public async Task<IActionResult> getStatsByKey(Guid? workoutId, string? key)
        {
            if (workoutId == null || key == null) return NotFound();

            var workoutOptionsByKey = await _context.WorkoutsOptions.Where(o => o.WorkoutId== workoutId && o.Key == key).OrderBy(o=>o.CreatedAt).FirstOrDefaultAsync();

            return Json(workoutOptionsByKey);
        }

        [HttpPost]
        public async Task<IActionResult> add(Guid? workoutId, [Bind("Key,Value")] WorkoutOption option )
        {

            if (workoutId == null) return NotFound();

            option.WorkoutId = (Guid)workoutId;

            try
            {
                _context.WorkoutsOptions.Add(option);
                await _context.SaveChangesAsync();
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }

            return Json(option);
        }

        [HttpPut]
        public async Task<IActionResult> update(Guid? workoutId, [Bind("WorkoutOptionId,key,value,CreatedAt")] WorkoutOption option)
        {

            if (workoutId == null) return NotFound();

            option.WorkoutId = (Guid)workoutId;

            try
            {
                _context.WorkoutsOptions.Update(option);
                await _context.SaveChangesAsync();
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }

            return Json(option);
        }
    }
}
