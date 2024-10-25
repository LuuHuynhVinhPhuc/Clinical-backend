using ClinicalBackend.Domain.Entities;
using Domain.Interfaces;

namespace ClinicalBackend.Domain.Repositories
{
    public interface IMedicineRepository : IBaseRepository<Medicine>
    {
        Task<IEnumerable<Medicine>> SearchByNameAsync(string Name, int pageNumber, int pageSize);
        Task<IEnumerable<Medicine>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default); 
        Task<Medicine> GetByIdAsync(Guid Id);
        Task<int> GetTotalCountAsync(); 
        Task<int> GetTotalCountByNameAsync(string Name);
        Task<int> GetTotalCountByDateAsync(DateTime dateStart, DateTime dateEnd);
        Task<IEnumerable<Medicine>> GetMedicinesByDateAsync(DateTime dateStart, DateTime dateEnd, int pageNumber, int pageSize);
    }
}