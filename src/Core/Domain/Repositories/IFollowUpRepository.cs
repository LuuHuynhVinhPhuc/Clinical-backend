using ClinicalBackend.Domain.Entities;
using Domain.Interfaces;

namespace ClinicalBackend.Domain.Repositories
{
    public interface IFollowUpRepository : IBaseRepository<FollowUp>
    {
        Task<IEnumerable<FollowUp>> GetAllAsync();

        Task<FollowUp> GetByIdAsync(Guid Id);
    }
}