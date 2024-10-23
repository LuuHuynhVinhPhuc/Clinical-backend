using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Domain.Repositories;
using ClinicalBackend.Persistence.Context;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace ClinicalBackend.Persistence.Repositories
{
    public class PrescriptionRepository : BaseRepository<Prescription>, IPrescriptionRepository
    {
        public PrescriptionRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Prescription>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            return await dbSet
                .Include(p => p.Patient)
                .Include(p => p.FollowUp)
                .Include(p => p.Products)
                .ThenInclude(p => p.Medicine)
                .OrderByDescending(p => p.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<Prescription> GetByIdAsync(Guid Id)
        {
            return await dbSet
                .Include(p => p.Patient)
                .Include(p => p.FollowUp)
                .Include(p => p.Products)
                .ThenInclude(p => p.Medicine)
                .FirstOrDefaultAsync(p => p.Id == Id)
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Prescription>> GetByPatientIdAsync(Guid patientId)
        {
            return await dbSet
                .Include(p => p.Patient)
                .Include(p => p.FollowUp)
                .Include(p => p.Products)
                .ThenInclude(p => p.Medicine)
                .Where(p => p.PatientId == patientId)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<int> GetTotalCountAsync() // Add this method
        {
            return await dbSet.CountAsync().ConfigureAwait(false);
        }

        public async Task<int> GetTotalCountByNameAsync(string Name)
        {
            return await dbSet.CountAsync(p => p.Patient.Name == Name).ConfigureAwait(false);
        }


        public async Task<int> GetTotalCountByPatientIdAsync(Guid patientId)
        {
            return await dbSet.CountAsync(p => p.PatientId == patientId).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Prescription>> SearchByNameAsync(string Name, int pageNumber, int pageSize)
        {
            return await dbSet
                .Include(p => p.Patient)
                .Include(p => p.FollowUp)
                .Include(p => p.Products)
                .ThenInclude(p => p.Medicine)
                .Where(p => p.Patient.Name == Name)
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }
}