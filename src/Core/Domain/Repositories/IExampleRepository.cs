using ClinicalBackend.Domain.Entities;
using Domain.Interfaces;

namespace ClinicalBackend.Domain.Repositories
{
    public interface IExampleRepository : IBaseRepository<Example>
    {
        Task<Example> GetByIdAsync(int id);
    }
}