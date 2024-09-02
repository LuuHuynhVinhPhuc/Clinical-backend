using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Domain.Repositories;
using ClinicalBackend.Persistence.Context;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace ClinicalBackend.Persistence.Repositories
{
    internal class FollowUpRepository : BaseRepository<FollowUp>, IFollowUpRepository
    {
        internal FollowUpRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<FollowUp>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<FollowUp> GetByIdAsync(Guid Id)
        {
            return await dbSet.FindAsync();
        }

        public async Task<List<FollowUp>> SearchByNameAsync(string Name)
        {
            return await dbSet
                .Where(m => m.Name.Contains(name)) // Use Contains for partial matches
                .ToListAsync();        }
    }
}