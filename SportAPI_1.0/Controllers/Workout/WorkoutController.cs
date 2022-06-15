using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using PagedList;
using SportAPI.Interfaces;
using SportAPI.Models.User;
using SportAPI.Models.Workout;

namespace SportAPI.Controllers
{
    [Route("api/workout")]
    [ApiController]
    public class WorkoutController : ControllerBase
    {
        private readonly SportContext _context;
        private readonly IWorkoutService _workoutService;
        private readonly IUserService _userService;

        public WorkoutController(SportContext context, IUserService userService, IWorkoutService workoutService)
        {
            this._context = context;
            this._workoutService = workoutService;
            this._userService = userService;
        }

        // GET: User Workouts

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            User user = this._userService.GetByEmail(this.User.Identity.Name);

            var workouts = await this._workoutService
                .GetWorkouts()
                .Where(workout => workout.UserId == user.UserId)
                .ToListAsync();

            return this.Ok(workouts);
        }

        [Authorize]
        [HttpGet("{workoutID}")]
        public async Task<IActionResult> GetWorkout(Guid workoutID)
        {
            User user = this._userService.GetByEmail(this.User.Identity.Name);
            var workout = await this._workoutService.GetWorkoutById(user, workoutID);

            return this.Ok(workout);
        }

        [Authorize]
        [HttpGet("global")]
        public IActionResult GetWorkoutPublishedList(
            [FromQuery] int limit = 30,
            [FromQuery] int page = 1,
            [FromQuery] string search = null)
        {
            search = search?.ToLower();

            IQueryable<Models.Workout.Workout> query = this._workoutService
                .GetWorkouts()
                .Where(workout => workout.IsPublished == true);

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(
                    workout =>
                        workout.Name.ToLower().Contains(search) ||
                        workout.About.ToLower().Contains(search));
            }

            List<Models.Workout.Workout> workoutList = query
                .OrderByDescending(workout => workout.CreatedAt)
                .ToPagedList(
                    pageNumber: page,
                    pageSize: limit)
                .ToList();

            return this.Ok(workoutList);
        }

        [HttpPost]
        public async Task<IActionResult> Index([Bind("Name,About,IsPublished")] Models.Workout.Workout workout)
        {
            User user = this._userService.GetByEmail(this.User.Identity.Name);

            var res = await this._workoutService.AddWorkout(user, workout);

            return this.Ok(res);
        }

        //PUT: Edit User
        [HttpPut]
        public async Task<IActionResult> Edit([Bind("WorkoutId,Name,About,IsPublished,StartFrom")] Models.Workout.Workout workout)
        {
            User user = this._userService.GetByEmail(this.User.Identity.Name);

            var res = await this._workoutService.UpdateWorkout(user, workout);

            return this.Ok(res);
        }

        // HttpDelete: Delete Workout
        [HttpDelete("{workoutId}")]
        public async Task<IActionResult> Delete(Guid workoutId)
        {
            User user = this._userService.GetByEmail(this.User.Identity.Name);

            this._workoutService.DeleteWorkout(user, workoutId);

            return this.Ok(workoutId);
        }
    }
}