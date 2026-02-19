using FitCoders.Domain.Entities.Base;
using FitCoders.Domain.Enums;

namespace FitCoders.Domain.Entities
{
    public sealed class Member : BaseEntity
    {
        public string Name { get; private set; }
        public string Cpf { get; private set; }
        public string Email { get; private set; }
        public decimal Weight { get; private set; }
        public int Age { get; private set; }
        public DateOnly DateOfBirth { get; private set; }
        public Membership MembershipPlan { get; private set; }
        public DateOnly RenewalDate { get; private set; }
        public bool IsMembershipActive { get; private set; }
        public bool IsCoached { get; private set; }
        public Guid? InstructorId { get; private set; }
        public Instructor? Instructor { get; private set; } = null;

        private readonly List<Workout>? _workouts = new();
        public IReadOnlyCollection<Workout> Workouts => _workouts!.AsReadOnly();

        private Member() : base(default)
        {
            
        }

        public Member(Guid id, string name, string cpf, string email, DateTime dob, Membership plan, decimal weight, Instructor? instructor = null ,Workout? workout = null) : base(id)
        {
            Name = name;
            Cpf = cpf;
            Email = email;
            DateOfBirth = DateOnly.FromDateTime(dob);
            Age = CalculateAge(dob);
            RenewalDate = DateOnly.FromDateTime(CalculateRenewal(plan));
            Weight = weight;
            MembershipPlan = plan;
            IsMembershipActive = true;
            Instructor = instructor;
            InstructorId = instructor?.Id;
            IsCoached = instructor is not null ? true : false;
        }

        void AddInstructor(Instructor instructor)
        {
            Instructor = instructor ?? throw new ArgumentNullException(nameof(instructor));
            InstructorId = instructor.Id;

            IsCoached = true;
        }

        void RemoveInstructor()
        {
            Instructor = null;
            InstructorId = null;
            IsCoached = false;
        }

        void AddWorkout(Workout workout)
        {
            ArgumentNullException.ThrowIfNull(workout);

            if (!_workouts!.Any(w => w.Id == workout.Id))
            {
                _workouts!.Add(workout);
            }
        }
        void RemoveWorkout(Workout workout)
        {
            _workouts!.Remove(workout);
        }
        public void ClearWorkouts()
        {
            _workouts!.Clear();
        }

        static int CalculateAge(DateTime date)
        {
            int age = DateTime.Now.Year - date.Year;
            if (DateTime.Now.Year < date.DayOfYear)
                age--;
            return age;
        }

        public void RenewMembership()
        {
            RenewalDate = DateOnly.FromDateTime(CalculateRenewal(MembershipPlan));
            IsMembershipActive = true;
        }

        public void CancelMembership()
        {
            IsMembershipActive = false;
        }

        static DateTime CalculateRenewal(Membership plan)
        {
            var today = DateTime.Today;

            return plan switch
            {
                Membership.Monthly => today.AddMonths(1),
                Membership.Quarterly => today.AddMonths(3),
                Membership.Semiannual => today.AddMonths(6),
                Membership.Annual => today.AddYears(1),
                _ => throw new ArgumentException("Invalid Membership plan."),
            };
        }
    }
}