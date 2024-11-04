using FitCoders.Domain.Entities;
using FitCoders.Application.Repositories.Base;
using FitCoders.Domain.Enums;

namespace FitCoders.Application.Repositories
{
    public interface IMemberRepository : IAsyncRepository<Member>
    {
        Task <IReadOnlyList<Member>> GetMembersByMembershipAsync(Membership membership);
        Task<IReadOnlyList<Member>> GetMembersByInstructorAsync(Instructor instructor);
    }
}