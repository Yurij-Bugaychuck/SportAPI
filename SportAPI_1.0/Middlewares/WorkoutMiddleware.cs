using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SportAPI.Models;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SportAPI.Middlewares
{
    public class WorkoutMiddleware
    {
        private readonly RequestDelegate _next;
        public WorkoutMiddleware(RequestDelegate next)
        {
            _next = next;
            
        }

        public async Task InvokeAsync(HttpContext context, SportContext dbContext)
        {
            string path = context.Request.Path.Value.ToLower().TrimEnd('/');
            if (context.Request.Path.Value.StartsWith("/workout")) {
               
                var pa = path.Split('/');
               
                if (pa.Length > 2)
                {
                    try
                    {
                      

                        Guid? workoutID = Guid.Parse(pa[2]);
                        Guid userID = dbContext.Users.FirstOrDefault(o => o.Email == context.User.Identity.Name).UserId;
                        Guid? workoutUserID = dbContext.Workouts.FirstOrDefault(o => o.WorkoutId == workoutID).UserId;

                        if (workoutUserID == null || userID != workoutUserID)
                        {
                            throw new Exception("You don't have permission :(");
                        }
                        else
                        {
                            context.User.Claims.Append(new Claim("workout", workoutID.ToString()));
                        }                    

                    }
                    catch(Exception e)
                    {
                        context.Response.StatusCode = 403;
                        context.Response.WriteAsync(e.Message);
                        return;
                    }
                }
              
            }
           


            await _next(context);
        }

        private bool UserAccess(HttpContext context, SportContext _context, Guid? workoutId)
        {
            var workout = _context.Workouts.Where(o => o.WorkoutId == workoutId).FirstOrDefault();
            var user = _context.Users.FirstOrDefault(o => o.Email == context.User.Identity.Name);

            if (user.UserId != workout.UserId) return false;

            return true;
        }
    }
}
