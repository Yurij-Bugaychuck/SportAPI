using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportAPI.Models;
using SportAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using SportAPI.Models.User;
using SportAPI.Models.Workout;

namespace SportAPI.Controllers
{
    [Route("api/workout/{workoutId}/option")]
    [ApiController]
    [Authorize]
    public class WorkoutOptionsController : ControllerBase
    {
        private readonly SportContext _context;
        private readonly IWorkoutService _workoutService;
        private readonly IUserService _userService;

        public WorkoutOptionsController(SportContext context, IWorkoutService workoutService, IUserService userService)
        {
            this._context = context;
            this._workoutService = workoutService;
            this._userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid workoutId)
        {
            User user = this._userService.GetByEmail(this.User.Identity.Name);
            var res = await this._workoutService.GetWorkoutOptions(user, workoutId);

            return this.Ok(res);
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> getStatsByKey(Guid workoutId, string key)
        {
            if (workoutId == null || key == null)
                return this.NotFound();

            User user = this._userService.GetByEmail(this.User.Identity.Name);

            var workoutOptionsByKey = await this._workoutService.GetWorkoutOptionByKey(user, workoutId, key);

            return this.Ok(workoutOptionsByKey);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Guid workoutId, [Bind("Key,Value")] WorkoutOption option)
        {
            User user = this._userService.GetByEmail(this.User.Identity.Name);
            this._workoutService.AddWorkoutOption(user, workoutId, option);

            return this.Ok(option);
        }

        [HttpPut]
        public async Task<IActionResult> Update(
            Guid workoutId, [Bind("WorkoutOptionId,key,value,CreatedAt")] WorkoutOption option)
        {
            User user = this._userService.GetByEmail(this.User.Identity.Name);
            this._workoutService.UpdateWorkoutOption(user, workoutId, option);

            return this.Ok(option);
        }

        [HttpDelete("{optionId}")]
        public async Task<IActionResult> Update(Guid workoutId, Guid optionId)
        {
            User user = this._userService.GetByEmail(this.User.Identity.Name);
            this._workoutService.DeleteWorkoutOption(user, workoutId, optionId);

            return this.Ok("deleted");
        }
    }
}