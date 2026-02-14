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
        public Workout(int id, string name, WorkoutType type) : base(id)
        {
            Name = name;
            Type = type;
        }

        public string Name { get; private set; }
        private readonly List<Exercise> _exercises = new();
        public IReadOnlyCollection<Exercise> Exercises => _exercises.AsReadOnly();
        public WorkoutType Type { get; private set; }

        internal void AddExercise (Exercise exercise)
        {
            ArgumentNullException.ThrowIfNull(exercise);
            _exercises.Add(exercise);
        }

        internal void DeleteExercise(Exercise exercise)
        {
            _exercises.Remove(exercise);
        }
    }
}