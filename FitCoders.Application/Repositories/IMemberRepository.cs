using FitCoders.Domain.Entities;
using FitCoders.Application.Repositories.Base;

namespace FitCoders.Application.Repositories
{
    public interface IMemberRepository : IAsyncRepository<Member>
    {
        
    }
}