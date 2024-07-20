using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitCoders.Application.Entities;
using FitCoders.Application.Repositories.Base;

namespace FitCoders.Application.Repositories
{
    public interface IMemberRepository : IAsyncRepository<Member>
    {
        
    }
}