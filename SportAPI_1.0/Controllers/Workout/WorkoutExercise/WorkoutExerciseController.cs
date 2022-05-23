using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SportAPI.Models;
using SportAPI.Interfaces;
using SportAPI.Models.User;

namespace SportAPI.Controllers
{
    [Route("api/workout/{workoutId}/exercises")]
    [ApiController]
    public class WorkoutExerciseController : ControllerBase
    {
        private readonly IWorkoutService _workoutService;
        private readonly IUserService _userService;
        public WorkoutExerciseController(IUserService userService, IWorkoutService workoutService)
        {
            this._workoutService = workoutService;
            this._userService = userService;
        }
        // GET: api/<WorkoutExcerciseController>
        [HttpGet]
        public async Task<IActionResult> Get(Guid workoutId)
        {
            User user = this._userService.GetByEmail(this.User.Identity.Name);

            var excercises = this._workoutService.GetWorkoutExercises(user, workoutId);

            return this.Ok(excercises);
        }

        // GET api/<WorkoutExcerciseController>/5
        [HttpGet("{exerciseId}")]
        public async Task<IActionResult> GetExercise(Guid workoutId, Guid exerciseId)
        {
            User user = this._userService.GetByEmail(this.User.Identity.Name);
            var exercise = this._workoutService.GetWorkoutExerciseById(user, workoutId, exerciseId);

            return this.Ok(exercise);
        }

        // POST api/<WorkoutExcerciseController>
        [HttpPost]
        public async Task<IActionResult> Post(Guid workoutId, [Bind("IsSet,Order,Repeats,Calories,Sets,Duration,Weight,Name,About")] WorkoutExcercise excercise)
        {
            User user = this._userService.GetByEmail(this.User.Identity.Name);

            excercise.WorkoutId = workoutId;
            
            excercise  = this._workoutService.AddWorkoutExercise(user, excercise);

            return this.Ok(excercise);
        }

        [HttpPost("range")]
        public async Task<IActionResult> PostRange(Guid workoutId, [Bind("IsSet,Order,Repeats,Calories,Sets,Duration,Weight,Name,About")] List<WorkoutExcercise> excercises)
        {
            User user = this._userService.GetByEmail(this.User.Identity.Name);

            excercises.ForEach(o => o.WorkoutId = workoutId);

            excercises = this._workoutService.AddRangeWorkoutExercise(user, excercises);

            return this.Ok(excercises);
        }

        // PUT api/<WorkoutExcerciseController>/5
        [HttpPut("{exerciseId}")]
        public async Task<IActionResult> Put(Guid workoutId, Guid exerciseId, [Bind("IsSet,Order,Repeats,Calories,Sets,Duration,Weight,Name,About")] WorkoutExcercise exercise)
        {
            User user = this._userService.GetByEmail(this.User.Identity.Name);

            exercise.WorkoutId = workoutId;
            exercise.WorkoutExcerciseId = exerciseId;

            var excercise = this._workoutService.UpdateWorkoutExercise(user, exercise);

            return this.Ok(excercise);
        }

        // DELETE api/<WorkoutExcerciseController>/5
        [HttpDelete("{exerciseId}")]
        public async Task<IActionResult> Delete(Guid workoutId, Guid exerciseId)
        {
            User user = this._userService.GetByEmail(this.User.Identity.Name);

            var excercise = this._workoutService.GetWorkoutExerciseById(user, workoutId, exerciseId);

            excercise = this._workoutService.RemoveWorkoutExercise(user, excercise);

            return this.Ok("deleted " + exerciseId);
        }
    }
}
