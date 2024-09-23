using ClinicalBackend.Domain.Entities;
using Domain.Interfaces;

namespace ClinicalBackend.Domain.Repositories
{
    public interface IFollowUpRepository : IBaseRepository<FollowUp>
    {
        Task<IEnumerable<FollowUp>> GetAllAsync(int pageNumber, int pageSize);
        Task<FollowUp> GetByIdAsync(Guid Id);
        Task<int> GetTotalCountAsync();
    }
}