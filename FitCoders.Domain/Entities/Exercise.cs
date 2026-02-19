using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitCoders.Domain.Entities.Base;

namespace FitCoders.Domain.Entities
{
    //TODO: Add a URL link (in the frontend) redirecting to a video showcasing the exercise execution.
    public sealed class Exercise : BaseEntity
    {
        public Exercise(Guid id, string name, int sets, int reps, int rest) :base(id)
        {
            Name = name;
            Sets = sets;
            Reps = reps;
            Rest = rest;
        }
        
        public string Name { get; private set; }
        public int Sets { get; private set; }
        public int Reps { get; private set; }
        public int Rest { get; private set; }
    }
}