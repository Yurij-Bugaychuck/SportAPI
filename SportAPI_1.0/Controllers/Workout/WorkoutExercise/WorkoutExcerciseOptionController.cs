using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportAPI.Models;
using SportAPI.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SportAPI.Controllers.Workout.WorkoutExercise
{
    [Route("api/workout/{workoutId}/exercise/{exerciseId}/options")]
    [ApiController]
    public class WorkoutExcerciseOptionController : ControllerBase
    {
        // GET: api/<WorkoutExcerciseOptionController>

        private readonly IWorkoutService _workoutService;
        private readonly IUserService _userService;

        public WorkoutExcerciseOptionController(IWorkoutService workoutService, IUserService userService)
        {
            _workoutService = workoutService;
            _userService = userService;
        }
    
        [HttpGet]
        public async Task<IActionResult> Get(Guid workoutId, Guid exerciseId)
        {
            User user = _userService.GetByEmail(User.Identity.Name);
            var p = _workoutService.GetWorkoutExerciseOptions(user, workoutId, exerciseId);

            return Ok(p);
        }

        // GET api/<WorkoutExcerciseOptionController>/5
        [HttpGet("{key}")]
        public async Task<IActionResult> Get(Guid workoutId, Guid exerciseId, string key)
        {
            User user = _userService.GetByEmail(User.Identity.Name);
            var p = _workoutService.GetWorkoutExerciseOptionByKey(user, workoutId, exerciseId, key);

            return Ok(p);
        }

        // POST api/<WorkoutExcerciseOptionController>
        [HttpPost]
        public async Task<IActionResult> Post(Guid workoutId, Guid exerciseId, [FromBody] WorkoutExcerciseOption option)
        {
            User user = _userService.GetByEmail(User.Identity.Name);
            option.WorkoutExcerciseId = exerciseId;
            var res = _workoutService.AddWorkoutExerciseOption(user, workoutId, exerciseId, option);

            return Ok("Added");
        }

        // PUT api/<WorkoutExcerciseOptionController>/5
        [HttpPut]
        public async Task<IActionResult> Put(Guid workoutId, Guid exerciseId, [FromBody] WorkoutExcerciseOption option)
        {
            User user = _userService.GetByEmail(User.Identity.Name);
            option.WorkoutExcerciseId = exerciseId;
            var res = _workoutService.UpdateWorkoutExerciseOption(user, workoutId, exerciseId, option);

            return Ok("Update");
        }

        // DELETE api/<WorkoutExcerciseOptionController>/5
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid workoutId, Guid exerciseId, [FromBody] WorkoutExcerciseOption option)
        {
            User user = _userService.GetByEmail(User.Identity.Name);
            option.WorkoutExcerciseId = exerciseId;
            var res = _workoutService.RemoveWorkoutExerciseOption(user, workoutId, exerciseId, option);

            return Ok("Deleted");
        }
    }
}
