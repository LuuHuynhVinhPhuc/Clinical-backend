using ClinicalBackend.Domain.Entities;
using Domain.Interfaces;

namespace ClinicalBackend.Domain.Repositories
{
    public interface IFollowUpRepository : IBaseRepository<FollowUp>
    {
        Task<IEnumerable<FollowUp>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
        Task<FollowUp> GetByIdAsync(Guid Id);
        Task<IEnumerable<FollowUp>> GetByPatientIdAsync(Guid patientId);
        Task<int> GetTotalCountAsync();
    }
}