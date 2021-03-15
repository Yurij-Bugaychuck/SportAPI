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


namespace SportAPI.Controllers
{
    [Route("workout")]
    public class WorkoutController : Controller
    {
        private readonly SportContext _context;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly ImageService _ImageService;
        public WorkoutController(SportContext context, IWebHostEnvironment appEnvironment, ImageService imageService)
        {
            _context = context;
            _appEnvironment = appEnvironment;
            _ImageService = imageService;

        }

        // GET: User Workouts
        
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            User user = await GetUser();
            var workouts = _context.Workouts.Where(w => w.UserId == user.UserId).ToList();
            return Json(workouts);
        }
        
        [HttpGet("{workoutID?}")]
        public async Task<IActionResult> getWorkot(Guid? workoutID)
        {
            var workout = _context.Workouts.FirstOrDefault(o => o.WorkoutId == workoutID);
            return Json(workout);
        }
        
        [HttpPost]
        public async Task<IActionResult> Index([Bind("Name,About,isPublished")] Workout workout)
        {
            var user = await GetUser();
            workout.UserId = user.UserId;
            _context.Workouts.Add(workout);
            await _context.SaveChangesAsync();
            return Json(workout);
        }

        //PUT: Edit User
        [HttpPut]
       
        public async Task<IActionResult> Edit([Bind("WorkoutId,UserId,Name,About,CreatedAt,IsPublished")] Workout workout)
        {   
            var user = await GetUser();

            Workout? workoutDB = _context.Workouts.Where(o => o.WorkoutId == workout.WorkoutId).FirstOrDefault();
            if (workoutDB == null) return NotFound("Can't find Workout with this ID");

            if (workoutDB.UserId != user.UserId) return Forbid("You not a onwer");

            workoutDB.Name = workout.Name;
            workoutDB.About = workout.About;
            workoutDB.CreatedAt = workout.CreatedAt;
            workoutDB.IsPublished = workout.IsPublished;


            try
            {
                _context.Workouts.Update(workoutDB);
                await _context.SaveChangesAsync();
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
            


            return Json(workoutDB);
        }

        // HttpDelete: Delete Workout
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid? workoutId)
        {

            var workoutDB = _context.Workouts.Where(o => o.WorkoutId == workoutId).FirstOrDefault();
            var user = await GetUser();

            if (workoutDB == null) return NotFound("Can't find Workout with this ID");

            if (workoutDB.UserId != user.UserId) return Forbid("You not a onwer");


            try
            {
                _context.Workouts.Remove(workoutDB);
                await _context.SaveChangesAsync();
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }


            return Json(workoutDB);
        }

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
        private async Task<User> GetUser(Guid? id = null)
        {
          
                return await _context.Users.FirstOrDefaultAsync(e => e.Email == User.Identity.Name);
            
        }
    }

}
