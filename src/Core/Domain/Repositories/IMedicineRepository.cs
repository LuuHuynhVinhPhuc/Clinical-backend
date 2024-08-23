using ClinicalBackend.Domain.Entities;
using Domain.Interfaces;

namespace ClinicalBackend.Domain.Repositories
{
    public interface IMedicineRepository : IBaseRepository<Medicine>
    {
        Task<List<Medicine>> SearchByNameAsync(string Name);
        Task<IEnumerable<Medicine>> GetAllAsync();
        Task<Medicine> GetByIdAsync(Guid Id);
    }
}