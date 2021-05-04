using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportAPI.Models;
using SportAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
            _context = context;
            _workoutService = workoutService;
            _userService = userService;
        }

 
        [HttpGet]
        public async Task<IActionResult> Get(Guid workoutId)
        {
            User user = _userService.GetByEmail(User.Identity.Name);
            var res = await _workoutService.GetWorkoutOptions(user, workoutId);
           
            return Ok(res);
        }
        

        [HttpGet("{key}")]
        public async Task<IActionResult> getStatsByKey(Guid workoutId, string key)
        {
            if (workoutId == null || key == null) return NotFound();
            
            User user = _userService.GetByEmail(User.Identity.Name);

            var workoutOptionsByKey = await _workoutService.GetWorkoutOptionByKey(user, workoutId, key);

            return Ok(workoutOptionsByKey);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Guid workoutId, [Bind("Key,Value")] WorkoutOption option )
        {
            User user = _userService.GetByEmail(User.Identity.Name);
            _workoutService.AddWorkoutOption(user, workoutId, option);

            return Ok(option);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Guid workoutId, [Bind("WorkoutOptionId,key,value,CreatedAt")] WorkoutOption option)
        {

            User user = _userService.GetByEmail(User.Identity.Name);
            _workoutService.UpdateWorkoutOption(user, workoutId, option);

            return Ok(option);
        }

        [HttpDelete("{optionId}")]
        public async Task<IActionResult> Update(Guid workoutId, Guid optionId)
        {
            User user = _userService.GetByEmail(User.Identity.Name);
            _workoutService.DeleteWorkoutOption(user, workoutId, optionId);

            return Ok("deleted");
        }
    }
}
