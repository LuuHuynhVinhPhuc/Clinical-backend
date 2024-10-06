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
            return await dbSet.FindAsync(id).ConfigureAwait(false);
        }

        // find all           
        public async Task<IEnumerable<Patient>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await dbSet
                .OrderByDescending(m => m.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        // Find with Name 
        public async Task<IEnumerable<Patient>> FindWithNameAsync(string name, int pageNumber, int pageSize)
        {
            return await dbSet
                .Where(m => m.Name.Contains(name))
                .OrderByDescending(m => m.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        // Find with Phone number 
        public async Task<Patient> FindWithPhoneNumberAsync(string phoneNumber)
        {
            return await dbSet.FirstOrDefaultAsync(p => p.PhoneNumber == phoneNumber).ConfigureAwait(false);
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await dbSet.CountAsync().ConfigureAwait(false);
        }

        // Get patient by date start and date end and check status is "examined"
        public async Task<IEnumerable<Patient>> GetPatientByDateAsync(DateTime dateStart, DateTime dateEnd, int pageNumber, int pageSize)
        {
            return await dbSet
                .Where(p => p.CreatedAt >= dateStart && p.CreatedAt <= dateEnd && p.CheckStatus == "examined")
                .OrderByDescending(p => p.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }
}