using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportAPI.Models;
using SportAPI.Models.Workout.WorkoutExercise;

namespace SportAPI.Controllers.Workout.WorkoutExercise
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutExerciseCategoryController : ControllerBase
    {
        private readonly SportContext _context;

        public WorkoutExerciseCategoryController(SportContext context)
        {
            this._context = context;
        }

        // GET: api/WorkoutExcerciseCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutExerciseCategory>>> GetWorkoutsExercisesCategories()
        {
            return await this._context.WorkoutsExerciseCategory.ToListAsync();
        }

        // GET: api/WorkoutExcerciseCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkoutExerciseCategory>> GetWorkoutExerciseCategories(Guid id)
        {
            var workoutExerciseCategories = await this._context.WorkoutsExerciseCategory.FindAsync(id);

            if (workoutExerciseCategories == null)
            {
                return this.NotFound();
            }

            return workoutExerciseCategories;
        }

        // PUT: api/WorkoutExcerciseCategories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkoutExerciseCategories(Guid id, string name)
        {
            WorkoutExerciseCategory workoutExerciseCategory = this._context.WorkoutsExerciseCategory
                .FirstOrDefault(predicate: category => category.WorkoutExerciseCategoryId == id);

            if (workoutExerciseCategory != null)
            {
                workoutExerciseCategory.Name = name;
                await this._context.SaveChangesAsync();

                return this.Ok(workoutExerciseCategory);
            }
            
            return this.NotFound();
        }

        // POST: api/WorkoutExcerciseCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WorkoutExerciseCategory>> PostWorkoutExerciseCategories(string name)
        {
            WorkoutExerciseCategory newWorkoutExerciseCategory = new()
            {
                Name = name
            };
            
            this._context.WorkoutsExerciseCategory.Add(newWorkoutExerciseCategory);
            await this._context.SaveChangesAsync();

            return this
                .Ok(newWorkoutExerciseCategory);
        }

        // DELETE: api/WorkoutExcerciseCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkoutExerciseCategories(Guid id)
        {
            var workoutExerciseCategories = await this._context.WorkoutsExerciseCategory.FindAsync(id);
            
            if (workoutExerciseCategories == null)
            {
                return this.NotFound();
            }

            this._context.WorkoutsExerciseCategory.Remove(workoutExerciseCategories);
            await this._context.SaveChangesAsync();

            return this.Ok($"Category Successfully deleted. Id: {id}.");
        }
    }
}
