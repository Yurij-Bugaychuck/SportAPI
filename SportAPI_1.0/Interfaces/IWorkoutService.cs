using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportAPI.Models.User;
using SportAPI.Models.Workout;
using SportAPI.Models.Workout.WorkoutExercise;

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

        public List<WorkoutExercise> GetWorkoutExercises(User user, Guid workoutId);

        WorkoutExercise GetWorkoutExerciseById(User user, Guid workoutId, Guid exerciseId);
        WorkoutExercise AddWorkoutExercise(User user, WorkoutExercise exercise);
        WorkoutExercise UpdateWorkoutExercise(User user, WorkoutExercise exercise);
        WorkoutExercise RemoveWorkoutExercise(User user, WorkoutExercise exercise);
        List<WorkoutExercise> AddRangeWorkoutExercise(User user, List<WorkoutExercise> exercises);

        List<WorkoutExerciseOption> GetWorkoutExerciseOptions(User user, Guid workoutId, Guid exerciseId);

        List<WorkoutExerciseOption> GetWorkoutExerciseOptionByKey(
            User user, Guid workoutId, Guid exerciseId, string key);

        WorkoutExerciseOption AddWorkoutExerciseOption(
            User user, Guid workoutId, Guid exerciseId, WorkoutExerciseOption option);

        WorkoutExerciseOption UpdateWorkoutExerciseOption(
            User user, Guid workoutId, Guid exerciseId, WorkoutExerciseOption option);

        WorkoutExerciseOption RemoveWorkoutExerciseOption(User user, Guid workoutId, Guid exerciseId, Guid optionId);
    }
}