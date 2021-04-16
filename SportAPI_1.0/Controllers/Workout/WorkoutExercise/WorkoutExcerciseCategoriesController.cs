using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportAPI.Models;

namespace SportAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutExcerciseCategoriesController : ControllerBase
    {
        private readonly SportContext _context;

        public WorkoutExcerciseCategoriesController(SportContext context)
        {
            _context = context;
        }

        // GET: api/WorkoutExcerciseCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutExcerciseCategories>>> GetWorkoutsExcercisesCategories()
        {
            return await _context.WorkoutsExcercisesCategories.ToListAsync();
        }

        // GET: api/WorkoutExcerciseCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkoutExcerciseCategories>> GetWorkoutExcerciseCategories(Guid id)
        {
            var workoutExcerciseCategories = await _context.WorkoutsExcercisesCategories.FindAsync(id);

            if (workoutExcerciseCategories == null)
            {
                return NotFound();
            }

            return workoutExcerciseCategories;
        }

        // PUT: api/WorkoutExcerciseCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkoutExcerciseCategories(Guid id, WorkoutExcerciseCategories workoutExcerciseCategories)
        {
            if (id != workoutExcerciseCategories.Workout_excercise_categories_id)
            {
                return BadRequest();
            }

            _context.Entry(workoutExcerciseCategories).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkoutExcerciseCategoriesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/WorkoutExcerciseCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WorkoutExcerciseCategories>> PostWorkoutExcerciseCategories(WorkoutExcerciseCategories workoutExcerciseCategories)
        {
            _context.WorkoutsExcercisesCategories.Add(workoutExcerciseCategories);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkoutExcerciseCategories", new { id = workoutExcerciseCategories.Workout_excercise_categories_id }, workoutExcerciseCategories);
        }

        // DELETE: api/WorkoutExcerciseCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkoutExcerciseCategories(Guid id)
        {
            var workoutExcerciseCategories = await _context.WorkoutsExcercisesCategories.FindAsync(id);
            if (workoutExcerciseCategories == null)
            {
                return NotFound();
            }

            _context.WorkoutsExcercisesCategories.Remove(workoutExcerciseCategories);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkoutExcerciseCategoriesExists(Guid id)
        {
            return _context.WorkoutsExcercisesCategories.Any(e => e.Workout_excercise_categories_id == id);
        }
    }
}
