using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
using SportAPI.Interfaces;

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
            _context = context;
            _workoutService = workoutService;
            _userService = userService;

        }

        // GET: User Workouts
        
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            User user = _userService.GetByEmail(User.Identity.Name);
            
            var workouts = await _workoutService.GetWorkouts(user);
            return Ok(workouts);
        }
        
        [HttpGet("{workoutID}")]
        public async Task<IActionResult> GetWorkout(Guid workoutID)
        {
            User user = _userService.GetByEmail(User.Identity.Name);
            var workout = await _workoutService.GetWorkoutById(user, workoutID);
            return Ok(workout);
        }
        
        [HttpPost]
        public async Task<IActionResult> Index([Bind("Name,About,IsPublished")] Workout workout)
        {
            User user = _userService.GetByEmail(User.Identity.Name);

            var res = await _workoutService.AddWorkout(user, workout);
            return Ok(res);
        }   

        //PUT: Edit User
        [HttpPut]
       
        public async Task<IActionResult> Edit([Bind("WorkoutId,Name,About,IsPublished")] Workout workout)
        {
            User user = _userService.GetByEmail(User.Identity.Name);

            var res = await _workoutService.UpdateWorkout(user, workout);

            return Ok(res);
        }

        // HttpDelete: Delete Workout
        [HttpDelete("{workoutId}")]
        public async Task<IActionResult> Delete(Guid workoutId)
        {
            User user = _userService.GetByEmail(User.Identity.Name);

             _workoutService.DeleteWorkout(user, workoutId);

            return Ok(workoutId);
        }

    }

}
