using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Domain.Repositories;
using ClinicalBackend.Persistence.Context;
using Domain.Common;

namespace ClinicalBackend.Persistence.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext context) : base(context)
        {
        }


        public async Task<Role> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id).ConfigureAwait(false);
        }
    }
}