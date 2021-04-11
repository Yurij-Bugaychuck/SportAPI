using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportAPI.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using SportAPI.Interfaces;
using System.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;
namespace SportAPI
{
    public class WorkoutService : IWorkoutService, ISecurityService
    {
        private SportContext _context;
        public WorkoutService(SportContext context)
        {
            _context = context;
        }
        public bool CanRead<T>(User user, T t)
        {
            return true;
        }

        public bool HaveAccessWorkout(User user, Workout workout) {
            if (user.Workouts.Contains(workout)) return true;
            return false;
        }

        public bool HaveAccessWorkout(User user, Guid workoutId)
        {
            Workout workout = _context.Workouts.FirstOrDefault(o => o.WorkoutId == workoutId);
            return HaveAccessWorkout(user, workout);
        }

        public bool HaveAccessWorkoutExercise(User user, Guid workoutId)
        {
            Workout workout = _context.Workouts.FirstOrDefault(o => o.WorkoutId == workoutId);
            return HaveAccessWorkout(user, workout);
        }


        public async Task<List<Workout>> GetWorkouts(User user)
        {
            return await _context.Workouts.Where(o => o.UserId == user.UserId).ToListAsync();
        }

        public async Task<Workout> GetWorkoutById(User user, Guid id)
        {
            return await _context.Workouts.Where(o => o.UserId == user.UserId).FirstOrDefaultAsync(o => o.WorkoutId == id);
        }

        public async Task<Workout> AddWorkout(User user, Workout workout)
        {
            workout.UserId = user.UserId;
            _context.Add(workout);
            await _context.SaveChangesAsync();

            return workout;
        }

        public async Task<Workout> UpdateWorkout(User user, Workout workout)
        {

            workout.UserId = user.UserId;
            _context.Workouts.Update(workout);
            await _context.SaveChangesAsync();

            return workout;



        }

        public async void DeleteWorkout(User user, Guid workoutID)
        {
            var workoutDB = await _context.Workouts.FirstOrDefaultAsync(o => o.WorkoutId == workoutID);

            if (workoutDB.UserId != user.UserId) throw new AuthenticationException();

            _context.Workouts.Remove(workoutDB);
            await _context.SaveChangesAsync();
        }

        public async Task<List<WorkoutOption>> GetWorkoutOptions(User user, Guid workoutId)
        {

            var workoutDB = _context.Workouts.Include(o => o.Options).FirstOrDefault(o => o.WorkoutId == workoutId && user.UserId == o.UserId);

            var workoutOptions = workoutDB.Options;


            return workoutOptions;
        }

        public async Task<List<WorkoutOption>> GetWorkoutOptionByKey(User user, Guid workoutId, string key)
        {

            var workoutDB = _context.Workouts.Include(o => o.Options).FirstOrDefault(o => o.WorkoutId == workoutId && user.UserId == o.UserId);

            if (workoutDB == null) throw new KeyNotFoundException();
            if (workoutDB.UserId != user.UserId) throw new AuthenticationException();

            var workoutOptions = workoutDB.Options.Where(o => o.Key == key).ToList();

            return workoutOptions;
        }


        public void AddWorkoutOption(User user, Guid workoutId, WorkoutOption option)
        {
            var workoutDB = _context.Workouts.FirstOrDefault(o => o.WorkoutId == workoutId);

            if (workoutDB == null) throw new KeyNotFoundException();
            if (workoutDB.UserId != user.UserId) throw new AuthenticationException();


            option.WorkoutId = (Guid) workoutId;

            _context.WorkoutsOptions.Add(option);
            _context.SaveChanges();

        }

        public void UpdateWorkoutOption(User user, Guid workoutId, WorkoutOption option)
        {
            var workoutDB = _context.Workouts.FirstOrDefault(o => o.WorkoutId == workoutId);

            if (workoutDB == null) throw new KeyNotFoundException();
            if (workoutDB.UserId != user.UserId) throw new AuthenticationException();


            option.WorkoutId = (Guid)workoutId;

            _context.WorkoutsOptions.Add(option);
            _context.SaveChanges();

        }

        public void DeleteWorkoutOption(User user, Guid workoutId, Guid optionId)
        {
            var workoutDB = _context.Workouts.FirstOrDefault(o => o.WorkoutId == workoutId);
            var optionDB = _context.WorkoutsOptions.FirstOrDefault(o => o.WorkoutOptionId == optionId);

            if (workoutDB == null) throw new KeyNotFoundException();
            if (optionDB == null) throw new KeyNotFoundException();
            if (workoutDB.UserId != user.UserId) throw new AuthenticationException();
            if (optionDB.WorkoutId != workoutId) throw new AuthenticationException();

            _context.WorkoutsOptions.Remove(optionDB);
        }


        public async Task<List<WorkoutExcercise>> GetWorkoutExercises(User user, Guid workoutId)
        {
            var workout = await _context.Workouts.Include(o => o.Excercises).FirstOrDefaultAsync(o => o.WorkoutId == workoutId);

            if (HaveAccessWorkout(user, workout))
            {
                return workout.Excercises.OrderBy(o=>o.Order).ToList();
            }
            else
            {
                throw new AuthenticationException();
            }
        }

        public async Task<WorkoutExcercise> GetWorkoutExerciseById(User user, Guid workoutId, Guid exerciseId)
        {
            var workout = await _context.Workouts.Include(o => o.Excercises).FirstOrDefaultAsync(o => o.WorkoutId == workoutId);

            if (HaveAccessWorkout(user, workout))
            {
                var exercise = workout.Excercises.FirstOrDefault(o => o.WorkoutExcerciseId == exerciseId);
                return exercise;
            }
            else
            {
                throw new AuthenticationException();
            }
        }

        public async Task<WorkoutExcercise> AddWorkoutExercise(User user, WorkoutExcercise exercise)
        {
            var workout = await _context.Workouts.Include(o => o.Excercises).FirstOrDefaultAsync(o => o.WorkoutId == exercise.WorkoutId);
            if (HaveAccessWorkout(user, workout))
            {
                _context.WorkoutsExcercises.Add(exercise);
                await _context.SaveChangesAsync();

                return exercise;
            }
            else
            {
                throw new AuthenticationException();
            }
        }

        public async Task<WorkoutExcercise> UpdateWorkoutExercise(User user, WorkoutExcercise exercise)
        {
            var workout = await _context.Workouts.Include(o => o.Excercises).FirstOrDefaultAsync(o => o.WorkoutId == exercise.WorkoutId);
            if (HaveAccessWorkout(user, workout))
            {
                _context.WorkoutsExcercises.Update(exercise);
                await _context.SaveChangesAsync();

                return exercise;
            }
            else
            {
                throw new AuthenticationException();
            }
        }

        public async Task<WorkoutExcercise> RemoveWorkoutExercise(User user, WorkoutExcercise exercise)
        {
            var workout = await _context.Workouts.Include(o => o.Excercises).FirstOrDefaultAsync(o => o.WorkoutId == exercise.WorkoutId);
            if (HaveAccessWorkout(user, workout))
            {
                _context.WorkoutsExcercises.Remove(exercise);
                await _context.SaveChangesAsync();

                return exercise;
            }
            else
            {
                throw new AuthenticationException();
            }
        }

        public async Task<List<WorkoutExcerciseOption>> GetWorkoutExerciseOptions(User user, Guid workoutId, Guid exerciseId)
        {
            var exercise = await _context.WorkoutsExcercises.Include(o=>o.Options).FirstOrDefaultAsync(o=> o.WorkoutExcerciseId == exerciseId && o.WorkoutId == workoutId);
            if (HaveAccessWorkout(user, workoutId))
            {
                if (exercise != null)
                {
                    return exercise.Options.OrderBy(o => o.CreatedAt).ToList();
                }
                else
                {
                    throw new AuthenticationException();
                }
            }
            else
            {
                throw new AuthenticationException();
            }
        }

        public async Task<List<WorkoutExcerciseOption>> GetWorkoutExerciseOptionByKey(
            User user, 
            Guid workoutId, 
            Guid exerciseId, 
            string key
            )
        {
            var exercise = await _context.WorkoutsExcercises.Include(o => o.Options).FirstOrDefaultAsync(o => o.WorkoutExcerciseId == exerciseId && o.WorkoutId == workoutId);
            if (HaveAccessWorkout(user, workoutId))
            {
                if (exercise != null)
                {
                    return exercise.Options.Where(o => o.Key == key).OrderBy(o=>o.CreatedAt).ToList();
                }
                else
                {
                    throw new AuthenticationException();
                }
            }
            else
            {
                throw new AuthenticationException();
            }
        }

        public async Task<WorkoutExcerciseOption> AddWorkoutExerciseOption(
            User user,
            Guid workoutId, 
            Guid exerciseId, 
            WorkoutExcerciseOption option
            )
        {
            var exercise = await _context.WorkoutsExcercises.Include(o => o.Options).FirstOrDefaultAsync(o => o.WorkoutExcerciseId == exerciseId && o.WorkoutId == workoutId);
            if (HaveAccessWorkout(user, workoutId))
            {
                if (exercise != null)
                {
                    _context.WorkoutsExcercisesOptions.Add(option);
                    await _context.SaveChangesAsync();

                    return option;
                }
                else
                {
                    throw new AuthenticationException();
                }
            }
            else
            {
                throw new AuthenticationException();
            }
        }

        public async Task<WorkoutExcerciseOption> UpdateWorkoutExerciseOption(
            User user,
            Guid workoutId,
            Guid exerciseId,
            WorkoutExcerciseOption option
            )
        {
            var exercise = await _context.WorkoutsExcercises.Include(o => o.Options).FirstOrDefaultAsync(o => o.WorkoutExcerciseId == exerciseId && o.WorkoutId == workoutId);
            if (HaveAccessWorkout(user, workoutId))
            {
                if (exercise != null)
                {
                    _context.WorkoutsExcercisesOptions.Update(option);
                    await _context.SaveChangesAsync();

                    return option;
                }
                else
                {
                    throw new AuthenticationException();
                }
            }
            else
            {
                throw new AuthenticationException();
            }
        }

        public async Task<WorkoutExcerciseOption> RemoveWorkoutExerciseOption(
            User user,
            Guid workoutId,
            Guid exerciseId,
            WorkoutExcerciseOption option
            )
        {
            var exercise = await _context.WorkoutsExcercises.Include(o => o.Options).FirstOrDefaultAsync(o => o.WorkoutExcerciseId == exerciseId && o.WorkoutId == workoutId);
            if (HaveAccessWorkout(user, workoutId))
            {
                if (exercise != null)
                {
                    var _option = exercise.Options.FirstOrDefault(o => o.WorkoutExcerciseOptionId == option.WorkoutExcerciseOptionId);

                    if (_option != null)
                    {
                        _context.WorkoutsExcercisesOptions.Remove(_option);

                        await _context.SaveChangesAsync();

                        return option;
                    }
                    
                }
            }
            
            throw new AuthenticationException();
        }
    }
}
