using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportAPI.Models;
namespace SportAPI.Interfaces
{
    public interface IWorkoutService
    {
        Task<List<Workout>> GetWorkouts(User user);
        Task<Workout> GetWorkoutById(User user, Guid id);
        Task<Workout> AddWorkout(User user, Workout workout);

        Task<Workout> UpdateWorkout(User user, Workout workout);
        void DeleteWorkout(User user, Guid id);


        public Task<List<WorkoutOption>> GetWorkoutOptions(User user, Guid workoutId);
        public Task<List<WorkoutOption>> GetWorkoutOptionByKey(User user, Guid workoutId, string key);

        void AddWorkoutOption(User user, Guid workoutId, WorkoutOption option);

        void UpdateWorkoutOption(User user, Guid workoutId, WorkoutOption option);

        void DeleteWorkoutOption(User user, Guid workoutId, Guid optionId);
    }
}
