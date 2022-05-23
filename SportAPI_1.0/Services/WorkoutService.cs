using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportAPI.Models;
using SportAPI.Interfaces;
using System.Data.Entity;
using System.Security.Authentication;
using SportAPI.Models.User;

namespace SportAPI
{
    public class WorkoutService : IWorkoutService
    {
        private SportContext _context;

        public WorkoutService(SportContext context)
        {
            this._context = context;
        }

        public bool HaveAccessWorkout(User user, Workout workout)
        {
            if (workout.UserId == user.UserId)
                return true;

            return false;
        }

        public bool HaveAccessWorkout(User user, Guid workoutId)
        {
            Workout workout = this._context.Workouts.FirstOrDefault(o => o.WorkoutId == workoutId);

            return this.HaveAccessWorkout(user, workout);
        }

        public bool HaveAccessWorkoutExercise(User user, Guid workoutId)
        {
            Workout workout = this._context.Workouts.FirstOrDefault(o => o.WorkoutId == workoutId);

            return this.HaveAccessWorkout(user, workout);
        }

        public IQueryable<Workout> GetWorkouts()
        {
            return this._context.Workouts.AsQueryable();
        }

        public async Task<Workout> GetWorkoutById(User user, Guid id)
        {
            return await this._context.Workouts.Where(o => o.UserId == user.UserId)
                .FirstOrDefaultAsync(o => o.WorkoutId == id);
        }

        public async Task<Workout> AddWorkout(User user, Workout workout)
        {
            workout.UserId = user.UserId;
            this._context.Add(workout);
            await this._context.SaveChangesAsync();

            return workout;
        }

        public async Task<Workout> UpdateWorkout(User user, Workout workout)
        {
            workout.UserId = user.UserId;

            var workoutdb = this._context.Workouts
                .FirstOrDefault(o => o.WorkoutId == workout.WorkoutId && user.UserId == o.UserId);

            if (workoutdb == null)
                throw new AuthenticationException();

            if (workout.Name != null)
                workoutdb.Name = workout.Name;

            if (workout.About != null)
                workoutdb.About = workout.About;

            workoutdb.IsPublished = workout.IsPublished;

            this._context.Workouts.Update(workoutdb);
            await this._context.SaveChangesAsync();

            return workout;
        }

        public void DeleteWorkout(User user, Guid workoutID)
        {
            var workoutDB = this._context.Workouts.FirstOrDefault(o => o.WorkoutId == workoutID);

            if (workoutDB == null)
            {
                throw new KeyNotFoundException();
            }

            if (workoutDB.UserId != user.UserId)
                throw new AuthenticationException();

            this._context.Workouts.Remove(workoutDB);
            this._context.SaveChanges();
        }

        public async Task<List<WorkoutOption>> GetWorkoutOptions(User user, Guid workoutId)
        {
            if (!this.HaveAccessWorkout(user, workoutId))
                throw new AuthenticationException();

            var workoutOptions = this._context.WorkoutsOptions.Where(o => o.WorkoutId == workoutId).ToList();

            return workoutOptions;
        }

        public async Task<List<WorkoutOption>> GetWorkoutOptionByKey(User user, Guid workoutId, string key)
        {
            if (!this.HaveAccessWorkout(user, workoutId))
                throw new AuthenticationException();

            var workoutOptions = this._context.WorkoutsOptions.Where(o => o.WorkoutId == workoutId && o.Key == key)
                .ToList();

            return workoutOptions;
        }

        public void AddWorkoutOption(User user, Guid workoutId, WorkoutOption option)
        {
            var workoutDB = this._context.Workouts.FirstOrDefault(o => o.WorkoutId == workoutId);

            if (workoutDB == null)
                throw new KeyNotFoundException();

            if (workoutDB.UserId != user.UserId)
                throw new AuthenticationException();

            option.WorkoutId = (Guid) workoutId;

            this._context.WorkoutsOptions.Add(option);
            this._context.SaveChanges();
        }

        public void UpdateWorkoutOption(User user, Guid workoutId, WorkoutOption option)
        {
            var workoutDB = this._context.Workouts.FirstOrDefault(o => o.WorkoutId == workoutId);

            if (workoutDB == null)
                throw new KeyNotFoundException();

            if (workoutDB.UserId != user.UserId)
                throw new AuthenticationException();

            option.WorkoutId = (Guid) workoutId;

            this._context.WorkoutsOptions.Add(option);
            this._context.SaveChanges();
        }

        public void DeleteWorkoutOption(User user, Guid workoutId, Guid optionId)
        {
            var workoutDB = this._context.Workouts.FirstOrDefault(o => o.WorkoutId == workoutId);
            var optionDB = this._context.WorkoutsOptions.FirstOrDefault(o => o.WorkoutOptionId == optionId);

            if (workoutDB == null)
                throw new KeyNotFoundException();

            if (optionDB == null)
                throw new KeyNotFoundException();

            if (workoutDB.UserId != user.UserId)
                throw new AuthenticationException();

            if (optionDB.WorkoutId != workoutId)
                throw new AuthenticationException();

            this._context.WorkoutsOptions.Remove(optionDB);
        }

        public List<WorkoutExcercise> GetWorkoutExercises(User user, Guid workoutId)
        {
            var workout = this._context.Workouts.FirstOrDefault(o => o.WorkoutId == workoutId);

            if (workout == null)
                throw new KeyNotFoundException();

            if (!this.HaveAccessWorkout(user, workout))
                throw new AuthenticationException();

            var excercises = this._context.WorkoutsExcercises.Where(o => o.WorkoutId == workout.WorkoutId)
                .OrderBy(o => o.Order)
                .Include(o => o.Options)
                .ToList();

            return excercises;
        }

        public WorkoutExcercise GetWorkoutExerciseById(User user, Guid workoutId, Guid exerciseId)
        {
            var workout = this._context.Workouts.FirstOrDefault(o => o.WorkoutId == workoutId);

            if (this.HaveAccessWorkout(user, workout))
            {
                var exercise = this._context.WorkoutsExcercises.FirstOrDefault(o => o.WorkoutExcerciseId == exerciseId);

                return exercise;
            }
            else
            {
                throw new AuthenticationException();
            }
        }

        public WorkoutExcercise AddWorkoutExercise(User user, WorkoutExcercise exercise)
        {
            var workout = this._context.Workouts.FirstOrDefault(o => o.WorkoutId == exercise.WorkoutId);

            if (this.HaveAccessWorkout(user, workout))
            {
                this._context.WorkoutsExcercises.Add(exercise);
                this._context.SaveChanges();

                return exercise;
            }
            else
            {
                throw new AuthenticationException();
            }
        }

        public List<WorkoutExcercise> AddRangeWorkoutExercise(User user, List<WorkoutExcercise> exercises)
        {
            var workout = this._context.Workouts.FirstOrDefault(o => o.WorkoutId == exercises[0].WorkoutId);

            if (this.HaveAccessWorkout(user, workout))
            {
                this._context.WorkoutsExcercises.AddRange(exercises);
                this._context.SaveChanges();

                return this._context.WorkoutsExcercises.Where(o => o.WorkoutId == workout.WorkoutId).ToList();
            }
            else
            {
                throw new AuthenticationException();
            }
        }

        public WorkoutExcercise UpdateWorkoutExercise(User user, WorkoutExcercise exercise)
        {
            var workout = this._context.Workouts.FirstOrDefault(o => o.WorkoutId == exercise.WorkoutId);

            if (workout == null)
                throw new KeyNotFoundException();

            if (!this.HaveAccessWorkout(user, workout))
                throw new AuthenticationException();

            var excerciseDb =
                this._context.WorkoutsExcercises.FirstOrDefault(
                    o => o.WorkoutExcerciseId == exercise.WorkoutExcerciseId);

            excerciseDb.IsSet = exercise.IsSet;

            if (exercise.Name != null)
                excerciseDb.Name = exercise.Name;

            if (exercise.About != null)
                excerciseDb.About = exercise.About;

            if (exercise.Order != null)
                excerciseDb.Order = exercise.Order;

            if (exercise.Repeats != null)
                excerciseDb.Repeats = exercise.Repeats;

            if (exercise.Calories != null)
                excerciseDb.Calories = exercise.Calories;

            if (exercise.Sets != null)
                excerciseDb.Sets = exercise.Sets;

            if (exercise.Duration != null)
                excerciseDb.Duration = exercise.Duration;

            if (exercise.Weight != null)
                excerciseDb.Weight = exercise.Weight;

            excerciseDb.Order = exercise.Order;

            this._context.WorkoutsExcercises.Update(excerciseDb);
            this._context.SaveChanges();

            return excerciseDb;
        }

        public WorkoutExcercise RemoveWorkoutExercise(User user, WorkoutExcercise exercise)
        {
            var workout = this._context.Workouts.FirstOrDefault(o => o.WorkoutId == exercise.WorkoutId);

            if (this.HaveAccessWorkout(user, workout))
            {
                this._context.WorkoutsExcercises.Remove(exercise);
                this._context.SaveChanges();

                return exercise;
            }
            else
            {
                throw new AuthenticationException();
            }
        }

        public List<WorkoutExcerciseOption> GetWorkoutExerciseOptions(User user, Guid workoutId, Guid exerciseId)
        {
            //return _context.WorkoutsExcercisesOptions.Include(o => o.WorkoutExcercise).ToList();
            var exercise = this._context.WorkoutsExcercisesOptions.Where(o => o.WorkoutExcerciseId == exerciseId);

            if (this.HaveAccessWorkout(user, workoutId))
            {
                if (exercise != null)
                {
                    return exercise.OrderBy(o => o.CreatedAt).Include(o => o.WorkoutExcercise).ToList();
                }
                else
                {
                    throw new KeyNotFoundException();
                }
            }
            else
            {
                throw new AuthenticationException();
            }
        }

        public List<WorkoutExcerciseOption> GetWorkoutExerciseOptionByKey(
            User user,
            Guid workoutId,
            Guid exerciseId,
            string key)
        {
            var exercise = this._context.WorkoutsExcercises.Include(o => o.Options)
                .FirstOrDefault(o => o.WorkoutExcerciseId == exerciseId && o.WorkoutId == workoutId);

            if (this.HaveAccessWorkout(user, workoutId))
            {
                if (exercise != null)
                {
                    return exercise.Options.Where(o => o.Key == key).OrderBy(o => o.CreatedAt).ToList();
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

        public WorkoutExcerciseOption AddWorkoutExerciseOption(
            User user,
            Guid workoutId,
            Guid exerciseId,
            WorkoutExcerciseOption option)
        {
            var exercise = this._context.WorkoutsExcercises.FirstOrDefault(
                o => o.WorkoutExcerciseId == exerciseId && o.WorkoutId == workoutId);

            if (this.HaveAccessWorkout(user, workoutId))
            {
                if (exercise != null)
                {
                    Console.WriteLine(exercise);
                    option.WorkoutExcercise = exercise;
                    this._context.WorkoutsExcercisesOptions.Add(option);
                    this._context.SaveChanges();

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

        public WorkoutExcerciseOption UpdateWorkoutExerciseOption(
            User user,
            Guid workoutId,
            Guid exerciseId,
            WorkoutExcerciseOption option)
        {
            var exercise = this._context.WorkoutsExcercises.Include(o => o.Options)
                .FirstOrDefault(o => o.WorkoutExcerciseId == exerciseId && o.WorkoutId == workoutId);

            if (this.HaveAccessWorkout(user, workoutId))
            {
                if (exercise != null)
                {
                    var opt = this._context.WorkoutsExcercisesOptions
                        .FirstOrDefault(
                            o => o.WorkoutExcerciseId == option.WorkoutExcerciseId &&
                                 o.WorkoutExcerciseOptionId == option.WorkoutExcerciseOptionId);

                    opt.Key = option.Key;
                    opt.Value = option.Value;
                    this._context.WorkoutsExcercisesOptions.Update(opt);

                    this._context.SaveChanges();

                    return opt;
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

        public WorkoutExcerciseOption RemoveWorkoutExerciseOption(
            User user,
            Guid workoutId,
            Guid exerciseId,
            Guid optionId)
        {
            var exercise = this._context.WorkoutsExcercises.FirstOrDefault(
                o => o.WorkoutExcerciseId == exerciseId && o.WorkoutId == workoutId);

            if (this.HaveAccessWorkout(user, workoutId))
            {
                if (exercise != null)
                {
                    var _option = this._context.WorkoutsExcercisesOptions.FirstOrDefault(
                        o => o.WorkoutExcerciseOptionId == optionId &&
                             exercise.WorkoutExcerciseId == o.WorkoutExcerciseId);

                    if (_option != null)
                    {
                        this._context.WorkoutsExcercisesOptions.Remove(_option);

                        this._context.SaveChanges();

                        return _option;
                    }
                }
            }

            throw new AuthenticationException();
        }
    }
}