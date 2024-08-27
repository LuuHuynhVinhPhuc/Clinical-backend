using ClinicalBackend.Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicalBackend.Domain.Repositories
{
    public interface IPatientInfoRepository : IBaseRepository<PatientsInfo>
    {
        Task<PatientsInfo> GetByIdAsync(Guid id);
        Task<IEnumerable<PatientsInfo>> GetAllAsync();

        // search with Name 
        Task<List<PatientsInfo>> FindWithNameAsync(string name);

        // search with phone number 
        Task<List<PatientsInfo>> FindWithPhoneNumberAsync(string phoneNumber);

    }
}
