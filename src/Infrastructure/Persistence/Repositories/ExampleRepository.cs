using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Domain.Repositories;
using ClinicalBackend.Persistence.Context;
using Domain.Common;

namespace ClinicalBackend.Persistence.Repositories
{
    internal class ExampleRepository : BaseRepository<Example>, IExampleRepository
    {
        internal ExampleRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Example> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }
    }
}
