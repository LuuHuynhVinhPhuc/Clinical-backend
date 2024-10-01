using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Domain.Repositories;
using ClinicalBackend.Persistence.Context;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace ClinicalBackend.Persistence.Repositories
{
    public class MedicineRepository : BaseRepository<Medicine>, IMedicineRepository
    {
        public MedicineRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Medicine>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await dbSet
                .OrderByDescending(m => m.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<Medicine> GetByIdAsync(Guid Id)
        {
            return await dbSet.FindAsync(Id).ConfigureAwait(false);
        }

        public async Task<List<Medicine>> SearchByNameAsync(string name, int pageNumber, int pageSize)
        {
            return await dbSet
                .Where(m => m.Name.Contains(name))
                .OrderByDescending(m => m.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await dbSet.CountAsync().ConfigureAwait(false);
        }

        public async Task<int> GetTotalCountByNameAsync(string Name)
        {
            return await dbSet.CountAsync(m => m.Name.Contains(Name)).ConfigureAwait(false);
        }
    }
}