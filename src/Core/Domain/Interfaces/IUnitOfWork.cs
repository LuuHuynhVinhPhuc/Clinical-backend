using ClinicalBackend.Domain.Repositories;

namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IExampleRepository ExampleRepository { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
