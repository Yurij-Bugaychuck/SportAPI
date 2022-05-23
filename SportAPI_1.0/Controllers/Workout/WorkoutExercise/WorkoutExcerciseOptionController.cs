using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using SportAPI.Models;
using SportAPI.Interfaces;
using SportAPI.Models.User;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SportAPI.Controllers
{
    [Route("api/workout/{workoutId}/exercises/{exerciseId}/options")]
    [ApiController]
    public class WorkoutExcerciseOptionController : ControllerBase
    {
        // GET: api/<WorkoutExcerciseOptionController>

        private readonly IWorkoutService _workoutService;
        private readonly IUserService _userService;

        public WorkoutExcerciseOptionController(IWorkoutService workoutService, IUserService userService)
        {
            this._workoutService = workoutService;
            this._userService = userService;
        }
    
        [HttpGet]
        public async Task<IActionResult> Get(Guid workoutId, Guid exerciseId)
        {
            User user = this._userService.GetByEmail(this.User.Identity.Name);
            var p = this._workoutService.GetWorkoutExerciseOptions(user, workoutId, exerciseId);

            return this.Ok(p);
        }

        // GET api/<WorkoutExcerciseOptionController>/5
        [HttpGet("{key}")]
        public async Task<IActionResult> Get(Guid workoutId, Guid exerciseId, string key)
        {
            User user = this._userService.GetByEmail(this.User.Identity.Name);
            var p = this._workoutService.GetWorkoutExerciseOptionByKey(user, workoutId, exerciseId, key);

            return this.Ok(p);
        }

        // POST api/<WorkoutExcerciseOptionController>
        [HttpPost]
        public async Task<IActionResult> Post(Guid workoutId, Guid exerciseId, [Bind("key,value")] WorkoutExcerciseOption option)
        {
            User user = this._userService.GetByEmail(this.User.Identity.Name);
            
            var res = this._workoutService.AddWorkoutExerciseOption(user, workoutId, exerciseId, option);

            return this.Ok("Added");
        }

        // PUT api/<WorkoutExcerciseOptionController>/5
        [HttpPut("{optionId}")]
        public async Task<IActionResult> Put(Guid workoutId, Guid exerciseId, Guid optionId, [Bind("key,value")] WorkoutExcerciseOption option)
        {
            User user = this._userService.GetByEmail(this.User.Identity.Name);
            option.WorkoutExcerciseId = exerciseId;
            option.WorkoutExcerciseOptionId = optionId;
            var res = this._workoutService.UpdateWorkoutExerciseOption(user, workoutId, exerciseId, option);

            return this.Ok("Update");
        }

        // DELETE api/<WorkoutExcerciseOptionController>/5
        [HttpDelete("{optionId}")]
        public async Task<IActionResult> Delete(Guid workoutId, Guid exerciseId, Guid optionId)
        {
            User user = this._userService.GetByEmail(this.User.Identity.Name);
            
            var res = this._workoutService.RemoveWorkoutExerciseOption(user, workoutId, exerciseId, optionId);

            return this.Ok("Deleted");
        }
    }
}
