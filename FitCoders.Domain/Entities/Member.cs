using FitCoders.Domain.Entities.Base;
using FitCoders.Domain.Enums;

namespace FitCoders.Domain.Entities
{
    public class Member : BaseEntity
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
        public Instructor? Coach { get; private set; }
        public Workout? Workout { get; private set; }

        public Member(int id, string name, string cpf, string email, decimal? weight, DateTime dob, Membership plan, bool isCoached, Workout workout) : base(id)
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
            IsCoached = isCoached;
            Workout = workout;
        }

        protected static int CalculateAge(DateTime date)
        {
            int age = DateTime.Now.Year - date.Year;
            if (DateTime.Now.Year < date.DayOfYear)
                age--;
            return age;
        }

        protected static DateTime CalculateRenewal(Membership plan)
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