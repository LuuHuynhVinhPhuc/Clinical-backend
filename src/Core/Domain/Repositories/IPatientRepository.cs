using ClinicalBackend.Domain.Entities;
using Domain.Interfaces;

namespace ClinicalBackend.Domain.Repositories
{
    public interface IPatientRepository : IBaseRepository<Patient>
    {
        Task<Patient> GetByIdAsync(Guid id);
        Task<IEnumerable<Patient>> GetAllAsync();

        // search with Name 
        Task<List<Patient>> FindWithNameAsync(string name);

        // search with phone number 
        Task<Patient> FindWithPhoneNumberAsync(string phoneNumber);

    }
}