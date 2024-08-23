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

        public async Task<IEnumerable<Medicine>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<Medicine> GetByIdAsync(Guid Id)
        {
            return await dbSet.FindAsync(Id);
        }

        public async Task<List<Medicine>> SearchByNameAsync(string name)
        {
            return await dbSet
                .Where(m => m.Name.Contains(name)) // Use Contains for partial matches
                .ToListAsync();
        }
    }
}