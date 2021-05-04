using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportAPI.Models;
using SportAPI.Interfaces;

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
            _workoutService = workoutService;
            _userService = userService;
        }
        // GET: api/<WorkoutExcerciseController>
        [HttpGet]
        public async Task<IActionResult> Get(Guid workoutId)
        {
            User user = _userService.GetByEmail(User.Identity.Name);

            var excercises = _workoutService.GetWorkoutExercises(user, workoutId);

            return Ok(excercises);
        }

        // GET api/<WorkoutExcerciseController>/5
        [HttpGet("{exerciseId}")]
        public async Task<IActionResult> GetExercise(Guid workoutId, Guid exerciseId)
        {
            User user = _userService.GetByEmail(User.Identity.Name);
            var exercise = _workoutService.GetWorkoutExerciseById(user, workoutId, exerciseId);

            return Ok(exercise);
        }

        // POST api/<WorkoutExcerciseController>
        [HttpPost]
        public async Task<IActionResult> Post(Guid workoutId, [Bind("IsSet,Order,Name,About")] WorkoutExcercise excercise)
        {
            User user = _userService.GetByEmail(User.Identity.Name);

            excercise.WorkoutId = workoutId;
            
            excercise  = _workoutService.AddWorkoutExercise(user, excercise);

            return Ok(excercise);
        }

        // PUT api/<WorkoutExcerciseController>/5
        [HttpPut("{exerciseId}")]
        public async Task<IActionResult> Put(Guid workoutId, Guid exerciseId, [Bind("IsSet,Order,Name,About")] WorkoutExcercise exercise)
        {
            User user = _userService.GetByEmail(User.Identity.Name);

            exercise.WorkoutId = workoutId;
            exercise.WorkoutExcerciseId = exerciseId;

            var excercise = _workoutService.UpdateWorkoutExercise(user, exercise);

            return Ok(excercise);
        }

        // DELETE api/<WorkoutExcerciseController>/5
        [HttpDelete("{exerciseId}")]
        public async Task<IActionResult> Delete(Guid workoutId, Guid exerciseId)
        {
            User user = _userService.GetByEmail(User.Identity.Name);

            var excercise = _workoutService.GetWorkoutExerciseById(user, workoutId, exerciseId);

            excercise = _workoutService.RemoveWorkoutExercise(user, excercise);

            return Ok("deleted " + exerciseId);
        }
    }
}
