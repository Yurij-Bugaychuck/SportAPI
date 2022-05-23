using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportAPI.Models;
using SportAPI.Models.User;

namespace SportAPI.Interfaces
{
    public interface IWorkoutService
    {
        IQueryable<Workout> GetWorkouts();
        Task<Workout> GetWorkoutById(User user, Guid id);
        Task<Workout> AddWorkout(User user, Workout workout);

        Task<Workout> UpdateWorkout(User user, Workout workout);
        void DeleteWorkout(User user, Guid id);

        public Task<List<WorkoutOption>> GetWorkoutOptions(User user, Guid workoutId);
        public Task<List<WorkoutOption>> GetWorkoutOptionByKey(User user, Guid workoutId, string key);

        void AddWorkoutOption(User user, Guid workoutId, WorkoutOption option);

        void UpdateWorkoutOption(User user, Guid workoutId, WorkoutOption option);

        void DeleteWorkoutOption(User user, Guid workoutId, Guid optionId);

        public List<WorkoutExcercise> GetWorkoutExercises(User user, Guid workoutId);

        WorkoutExcercise GetWorkoutExerciseById(User user, Guid workoutId, Guid exerciseId);
        WorkoutExcercise AddWorkoutExercise(User user, WorkoutExcercise exercise);
        WorkoutExcercise UpdateWorkoutExercise(User user, WorkoutExcercise exercise);
        WorkoutExcercise RemoveWorkoutExercise(User user, WorkoutExcercise exercise);
        List<WorkoutExcercise> AddRangeWorkoutExercise(User user, List<WorkoutExcercise> exercises);

        List<WorkoutExcerciseOption> GetWorkoutExerciseOptions(User user, Guid workoutId, Guid exerciseId);

        List<WorkoutExcerciseOption> GetWorkoutExerciseOptionByKey(
            User user, Guid workoutId, Guid exerciseId, string key);

        WorkoutExcerciseOption AddWorkoutExerciseOption(
            User user, Guid workoutId, Guid exerciseId, WorkoutExcerciseOption option);

        WorkoutExcerciseOption UpdateWorkoutExerciseOption(
            User user, Guid workoutId, Guid exerciseId, WorkoutExcerciseOption option);

        WorkoutExcerciseOption RemoveWorkoutExerciseOption(User user, Guid workoutId, Guid exerciseId, Guid optionId);
    }
}