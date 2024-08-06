using ClinicalBackend.Domain.Repositories;

namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IRoleRepository Roles { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
