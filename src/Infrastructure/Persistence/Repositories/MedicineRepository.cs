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

        public override async Task<IEnumerable<Medicine>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            return await dbSet
                .OrderByDescending(m => m.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<Medicine> GetByIdAsync(Guid Id)
        {
            return await dbSet.FindAsync(Id).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Medicine>> SearchByNameAsync(string name, int pageNumber, int pageSize)
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

        public async Task<int> GetTotalCountByDateAsync(DateTime dateStart, DateTime dateEnd)
        {
            return await dbSet
                .CountAsync(p => p.CreatedAt >= dateStart && p.CreatedAt <= dateEnd && p.Status == "SOLD")
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Medicine>> GetMedicinesByDateAsync(DateTime dateStart, DateTime dateEnd, int pageNumber, int pageSize)
        {
            return await dbSet
                .Where(p => p.CreatedAt >= dateStart && p.CreatedAt <= dateEnd && p.Status == "SOLD")
                .OrderByDescending(p => p.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Medicine>> SearchBySpecialtyAsync(string Specialty, int pageNumber, int pageSize)
        {
            return await dbSet
                .Where(m => m.Specialty.Contains(Specialty))
                .OrderByDescending(m => m.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<int> GetTotalCountBySpecialtyAsync(string Specialty)
        {
            return await dbSet.CountAsync(m => m.Specialty.Contains(Specialty)).ConfigureAwait(false);
        }

        public async Task<Medicine> GetByNameAsync(string name)
        {
            return await dbSet
                .FirstOrDefaultAsync(m => m.Name == name)
                .ConfigureAwait(false);
        }
    }
}