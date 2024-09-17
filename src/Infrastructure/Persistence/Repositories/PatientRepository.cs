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
        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            return await dbSet.ToListAsync().ConfigureAwait(false);
        }

        // Find with Name 
        public async Task<List<Patient>> FindWithNameAsync(string name)
        {
            return await dbSet
                .Where(m => m.Name.Contains(name)) // Use Contains for partial matches
                .ToListAsync().ConfigureAwait(false);
        }

        // Find with Phone number 
        public async Task<Patient> FindWithPhoneNumberAsync(string phoneNumber)
        {
            return await dbSet.FirstOrDefaultAsync(p => p.PhoneNumber == phoneNumber);
        }
    }
}