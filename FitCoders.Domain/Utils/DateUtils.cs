using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using FitCoders.Domain.Entities;
using FitCoders.Domain.Enums;

namespace FitCoders.Domain.Utils
{
    public static class DateUtils
    {
        public static int CalculateAge(DateTime date)
        {
            int age = DateTime.Now.Year - date.Year;
            if (DateTime.Now.Year < date.DayOfYear)
                age--;
            return age;
        }

        public static DateTime CalculateRenewal(Membership plan)
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