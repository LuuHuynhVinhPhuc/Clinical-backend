using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Domain.Repositories;
using ClinicalBackend.Persistence.Context;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicalBackend.Persistence.Repositories
{
    public class PatientRepository : BaseRepository<PatientsInfo>, IPatientInfoRepository
    {
        public PatientRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<PatientsInfo> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }
    }
}
