using ClinicalBackend.Domain.Entities;
using Domain.Interfaces;

namespace ClinicalBackend.Domain.Repositories
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        Task<Role> GetByIdAsync(int id);
    }
}