using ClinicalBackend.Domain.Entities;
using Domain.Interfaces;

namespace ClinicalBackend.Domain.Repositories
{
    public interface IMedicineRepository : IBaseRepository<Medicine>
    {
        Task<List<Medicine>> SearchByNameAsync(string Name, int pageNumber, int pageSize);
        Task<IEnumerable<Medicine>> GetAllAsync(int pageNumber, int pageSize);
        Task<Medicine> GetByIdAsync(Guid Id);
    }
}