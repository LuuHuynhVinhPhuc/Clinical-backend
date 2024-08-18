using ClinicalBackend.Domain.Entities;
using Domain.Interfaces;

namespace ClinicalBackend.Domain.Repositories
{
    public interface IMedicineRepository : IBaseRepository<Medicine>
    {
        Task<Medicine> GetByIdAsync(Guid id);
        Task<IEnumerable<Medicine>> GetAllAsync();
    }
}