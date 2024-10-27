using FitCoders.Domain.Enums;
using FitCoders.Domain.Utils;

namespace FitCoders.Domain.Entities
{
    public class Member : BaseEntity
    {
        //TODO: Implement workout routine (exercise list) and if member hired private coaching (by an instructor)
        public int MemberId { get; private set; }
        public string Name { get; private set; }
        public string Cpf { get; private set; }
        public string Email { get; private set; }
        public decimal? Weight { get; private set; }
        public int Age { get; private set; }
        public DateOnly DateOfBirth { get; private set; }
        public Membership MemberPlan { get; private set; }
        public DateOnly RenewalDate { get; private set; }
        public bool IsMembershipActive { get; private set; }

        public Member(string name, string cpf, string email, decimal? weight, DateTime dob, Membership plan)
        {
            Name = name;
            Cpf = cpf;
            Email = email;
            Age = DateUtils.CalculateAge(dob);
            DateOfBirth = DateOnly.FromDateTime(dob);
            RenewalDate = DateOnly.FromDateTime(DateUtils.CalculateRenewal(plan));
            MemberPlan = plan;
            IsMembershipActive = true;
        }
    }
}