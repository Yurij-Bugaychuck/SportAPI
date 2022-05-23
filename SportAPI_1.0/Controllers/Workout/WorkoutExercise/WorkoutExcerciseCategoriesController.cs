using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            this._context = context;
        }

        // GET: api/WorkoutExcerciseCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutExcerciseCategories>>> GetWorkoutsExcercisesCategories()
        {
            return await this._context.WorkoutsExcercisesCategories.ToListAsync();
        }

        // GET: api/WorkoutExcerciseCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkoutExcerciseCategories>> GetWorkoutExcerciseCategories(Guid id)
        {
            var workoutExcerciseCategories = await this._context.WorkoutsExcercisesCategories.FindAsync(id);

            if (workoutExcerciseCategories == null)
            {
                return this.NotFound();
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
                return this.BadRequest();
            }

            this._context.Entry(workoutExcerciseCategories).State = EntityState.Modified;

            try
            {
                await this._context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.WorkoutExcerciseCategoriesExists(id))
                {
                    return this.NotFound();
                }
                else
                {
                    throw;
                }
            }

            return this.NoContent();
        }

        // POST: api/WorkoutExcerciseCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WorkoutExcerciseCategories>> PostWorkoutExcerciseCategories(WorkoutExcerciseCategories workoutExcerciseCategories)
        {
            this._context.WorkoutsExcercisesCategories.Add(workoutExcerciseCategories);
            await this._context.SaveChangesAsync();

            return this.CreatedAtAction("GetWorkoutExcerciseCategories", new { id = workoutExcerciseCategories.Workout_excercise_categories_id }, workoutExcerciseCategories);
        }

        // DELETE: api/WorkoutExcerciseCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkoutExcerciseCategories(Guid id)
        {
            var workoutExcerciseCategories = await this._context.WorkoutsExcercisesCategories.FindAsync(id);
            if (workoutExcerciseCategories == null)
            {
                return this.NotFound();
            }

            this._context.WorkoutsExcercisesCategories.Remove(workoutExcerciseCategories);
            await this._context.SaveChangesAsync();

            return this.NoContent();
        }

        private bool WorkoutExcerciseCategoriesExists(Guid id)
        {
            return this._context.WorkoutsExcercisesCategories.Any(e => e.Workout_excercise_categories_id == id);
        }
    }
}
