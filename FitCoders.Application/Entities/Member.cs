using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FitCoders.Application.Enums;

namespace FitCoders.Application.Entities
{
    public class Member : BaseEntity
    {
        public int MemberId { get; set; }
        public required string Name { get; set; }
        public required string Cpf { get; set; }
        public required string Email { get; set; }
        public decimal? Weight { get; set; }
        public required DateTime DateOfBirth { get; set; }
        public required MembershipPlan MembershipPlan { get; set; }
        public DateTime RenewalDate { get; protected set; }
        public bool IsActive { get; protected set; }
    }
}