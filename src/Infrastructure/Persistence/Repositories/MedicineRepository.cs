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
                .OrderByDescending(m => m.CreatedAt) // Sort by CreatedAt descending
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
                .OrderByDescending(m => m.CreatedAt) // Sort by CreatedAt descending
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize) // Use Contains for partial matches
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }
}