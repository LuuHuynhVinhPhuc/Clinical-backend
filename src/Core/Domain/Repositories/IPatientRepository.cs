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
        Task<Patient> GetByIdAsync(Guid Id);
    }
}
