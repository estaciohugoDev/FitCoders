using FitCoders.Domain.Entities.Base;
using FitCoders.Domain.Enums;

namespace FitCoders.Domain.Entities
{
    public sealed class Member : BaseEntity
    {
        public string Name { get; private set; }
        public string Cpf { get; private set; }
        public string Email { get; private set; }
        public decimal? Weight { get; private set; }
        public int Age { get; private set; }
        public DateOnly DateOfBirth { get; private set; }
        public Membership MemberPlan { get; private set; }
        public DateOnly RenewalDate { get; private set; }
        public bool IsMembershipActive { get; private set; }
        public bool IsCoached { get; private set; }
        public Instructor? Instructor { get; private set; }
        public Workout? Workout { get; private set; }
        public Gym Gym { get; private set; }

        public Member(int id, string name, string cpf, string email, Gym gym , DateTime dob, Membership plan, bool isCoached, decimal? weight ,Instructor? instructor ,Workout? workout) : base(id)
        {
            Name = name;
            Cpf = cpf;
            Email = email;
            DateOfBirth = DateOnly.FromDateTime(dob);
            Age = CalculateAge(dob);
            RenewalDate = DateOnly.FromDateTime(CalculateRenewal(plan));
            Weight = weight;
            MemberPlan = plan;
            IsMembershipActive = true;
            Instructor = instructor;
            IsCoached = isCoached;
            Workout = workout;
            Gym = gym;
        }

        void AddCoach(Instructor instructor)
        {
            Instructor = instructor;
            IsCoached = true;
        }

        void RemoveCoach()
        {
            Instructor = null;
            IsCoached = false;
        }

        static int CalculateAge(DateTime date)
        {
            int age = DateTime.Now.Year - date.Year;
            if (DateTime.Now.Year < date.DayOfYear)
                age--;
            return age;
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