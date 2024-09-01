using ClinicalBackend.Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicalBackend.Domain.Repositories
{
    public interface IPatientRepository : IBaseRepository<Patient>
    {
        Task<Patient> GetByIdAsync(Guid id);
        Task<IEnumerable<Patient>> GetAllAsync();

        // search with Name 
        Task<List<Patient>> FindWithNameAsync(string name);

        // search with phone number 
        Task<List<Patient>> FindWithPhoneNumberAsync(string phoneNumber);

    }
}
