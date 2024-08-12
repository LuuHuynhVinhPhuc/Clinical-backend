using ClinicalBackend.Domain.Entities;
using Domain.Interfaces;

namespace ClinicalBackend.Domain.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByIdAsync(Guid userId);
    }
}
