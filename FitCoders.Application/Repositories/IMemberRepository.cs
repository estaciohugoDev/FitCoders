using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitCoders.Domain.Entities;
using FitCoders.Application.Repositories.Base;
using FitCoders.Domain;

namespace FitCoders.Application.Repositories
{
    public interface IMemberRepository : IAsyncRepository<Member>
    {
        
    }
}