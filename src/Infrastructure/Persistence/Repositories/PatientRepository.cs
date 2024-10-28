using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Domain.Repositories;
using ClinicalBackend.Persistence.Context;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace ClinicalBackend.Persistence.Repositories
{
    public class PatientRepository : BaseRepository<Patient>, IPatientRepository
    {
        public PatientRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Patient> GetByIdAsync(Guid id)
        {
            return await dbSet
                .Include(f => f.FollowUps)
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }
        public override async Task<IEnumerable<Patient>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            return await dbSet
                .Include(f => f.FollowUps)
                .OrderByDescending(m => m.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        // Find with Name 
        public async Task<IEnumerable<Patient>> FindWithNameAsync(string name, int pageNumber, int pageSize)
        {
            return await dbSet
                .Include(f => f.FollowUps)
                .Where(p => p.Name.Contains(name))
                .OrderByDescending(m => m.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        // Find with Phone number 
        public async Task<IEnumerable<Patient>> FindWithPhoneNumberAsync(string phoneNumber)
        {
            return await dbSet
                    .Include(f => f.FollowUps)
                    .Where(p => p.PhoneNumber == phoneNumber)
                    .ToListAsync()
                    .ConfigureAwait(false);
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await dbSet.CountAsync().ConfigureAwait(false);
        }

        public async Task<int> GetTotalCountByDateAsync(DateTime dateStart, DateTime dateEnd)
        {
            return await dbSet
                .CountAsync(p => p.CreatedAt >= dateStart && p.CreatedAt <= dateEnd && p.CheckStatus == "examined")
                .ConfigureAwait(false);
        }

        // Get patient by date start and date end and check status is "examined"
        public async Task<IEnumerable<Patient>> GetPatientByDateAsync(DateTime dateStart, DateTime dateEnd, int pageNumber, int pageSize)
        {
            return await dbSet
                .Include(f => f.FollowUps)
                .Where(p => p.CreatedAt >= dateStart && p.CreatedAt <= dateEnd && p.CheckStatus == "examined")
                .OrderByDescending(p => p.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }
}