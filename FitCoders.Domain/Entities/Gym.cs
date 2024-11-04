using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitCoders.Domain.Entities.Base;
using FitCoders.Domain.Enums;

namespace FitCoders.Domain.Entities
{
    //TODO: Implement Networth system, as time goes by and Members pay their fee, the Gym income grows larger.
    public class Gym : BaseEntity
    {
        public string Name { get; private set; } = string.Empty;
        public List<Member>? Members { get; private set; } = [];
        public List<Instructor>? Instructors { get; private set; } = [];
        public List<Modality> Modalities { get; private set; } = [Modality.WeightTraining,Modality.CrossFit,Modality.Muaythai];

        public Gym(int id) : base(id) {}
        public Gym(int id, string name, List<Instructor>? instructors) : base(id)
        {
            Name = name;
            Instructors = instructors;
        }

        void AddMember(Member member) => Members!.Add(member);
        void RemoveMember(Member member) => Members!.Remove(member);
        void AddInstructor(Instructor instructor) => Instructors!.Add(instructor);
        void RemoveInstructor(Instructor instructor) => Instructors!.Remove(instructor);
    }
}