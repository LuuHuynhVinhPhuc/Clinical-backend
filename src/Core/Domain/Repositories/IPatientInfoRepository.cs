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
        Task<PatientsInfo> GetByIdAsync(int id);
        Task<IEnumerable<PatientsInfo>> GetAllAsysnc();
    }
}
