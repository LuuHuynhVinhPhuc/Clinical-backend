using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Domain.Repositories;
using ClinicalBackend.Persistence.Context;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace ClinicalBackend.Persistence.Repositories
{
    public class FollowUpRepository : BaseRepository<FollowUp>, IFollowUpRepository
    {
        public FollowUpRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<FollowUp>> GetAllAsync()
        {
            return await dbSet.ToListAsync().ConfigureAwait(false);
        }

        public async Task<FollowUp> GetByIdAsync(Guid Id)
        {
            return await dbSet.FindAsync().ConfigureAwait(false);
        }
    }
}