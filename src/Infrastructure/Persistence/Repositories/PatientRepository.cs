using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Domain.Repositories;
using ClinicalBackend.Persistence.Context;
using Domain.Common;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicalBackend.Persistence.Repositories
{
    public class PatientRepository : BaseRepository<Patient>, IPatientRepository
    {
        public PatientRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Patient> GetByIdAsync(Guid id)
        {
            return await dbSet.FindAsync(id);
        }

        // find all           
        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        // Find with Name 
        public async Task<List<Patient>> FindWithNameAsync(string name)
        {
            return await dbSet
                .Where(m => m.Name.Contains(name)) // Use Contains for partial matches
                .ToListAsync();
        }

        // Find with Phone number 
        public async Task<List<Patient>> FindWithPhoneNumberAsync(string phoneNumber)
        {
            return await dbSet.Where(m => m.PhoneNumber.Contains(phoneNumber)).ToListAsync();
        }
    }
}
