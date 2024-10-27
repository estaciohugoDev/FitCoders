using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using FitCoders.Domain.Enums;
using FitCoders.Domain.Utils;

namespace FitCoders.Domain.Entities
{
    public class Instructor : BaseEntity
    {
        public int InstructorId { get; private set; }
        public string Name { get; private set; }
        public string Cpf { get; private set; }
        public string Email { get; private set; }
        public InstructorShift Shift { get; private set; }

        public Instructor(string name, string cpf, string email, InstructorShift shift)
        {
            Name = name;
            Cpf = cpf;
            Email = email;
            Shift = shift;
        }
    }
}