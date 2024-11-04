using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitCoders.Domain.Entities.Base;
using FitCoders.Domain.Enums;

namespace FitCoders.Domain.Entities
{
    public sealed class Workout : BaseEntity
    {
        public Workout(int id, string name, List<Exercise> exercises, WorkoutType type) : base(id)
        {
            Name = name;
            Exercises = exercises;
            Type = type;
        }

        public string Name { get; private set; }
        public List<Exercise> Exercises { get; private set; }
        public WorkoutType Type { get; private set; }

        void AddExercise (Exercise exercise)
        {
            Exercises.Add(exercise);
        }

        void DeleteExercise(Exercise exercise)
        {
            Exercises.Remove(exercise);
        }
    }
}