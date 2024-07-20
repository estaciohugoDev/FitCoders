using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitCoders.Application.Enums;

namespace FitCoders.Application.Entities
{
    public class Instructor : BaseEntity
    {
        public int InstructorId { get; set; }
        public required string Name { get; set; }
        public required string Cpf { get; set; }
        public required InstructorShift Shift { get; set; }
    }
}