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

        public string Name { get; private set; }
        private readonly List<Exercise> _exercises = new();
        public IReadOnlyCollection<Exercise> Exercises => _exercises.AsReadOnly();
        public WorkoutType Type { get; private set; }
        public Guid MemberId { get; private set; }
        public Member Member { get; private set; }
        private Workout() { }
        public Workout(Guid id, string name, WorkoutType type, Member member) : base(id)
        {
            Name = name;
            Type = type;
            SetMember(member);
        }


        public void AddExercise (Exercise exercise) 
        {
            ArgumentNullException.ThrowIfNull(exercise);
            _exercises.Add(exercise);
        }

        public void DeleteExercise(Exercise exercise)
        {
            _exercises.Remove(exercise);
        }
        
        private void SetMember(Member member)
        {
            Member = member ?? throw new ArgumentNullException(nameof(member));
            MemberId = member.Id;
        }
    }
}