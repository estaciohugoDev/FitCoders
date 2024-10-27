using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using FitCoders.Domain.Enums;
using FitCoders.Domain.Utils;

namespace FitCoders.Domain.Entities
{
    public class Member : BaseEntity
    {
        public int MemberId { get; private set; }
        public string Name { get; private set; }
        public string Cpf { get; private set; }
        public string Email { get; private set; }
        public decimal? Weight { get; private set; }
        public int Age { get; private set; }
        public DateOnly DateOfBirth { get; private set; }
        public Membership MemberPlan { get; private set; }
        public DateOnly RenewalDate { get; private set; }
        public bool IsActive { get; private set; }

        public Member(string name, string cpf, string email, decimal? weight, DateTime dob, Membership plan)
        {
            Name = name;
            Cpf = cpf;
            Email = email;
            Age = DateUtils.CalculateAge(dob);
            DateOfBirth = DateOnly.FromDateTime(dob);
            RenewalDate = DateOnly.FromDateTime(DateUtils.CalculateRenewal(plan));
            MemberPlan = plan;
            IsActive = true;
        }
    }
}