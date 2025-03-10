﻿using ClinicalBackend.Domain.Entities;
using Domain.Interfaces;

namespace ClinicalBackend.Domain.Repositories
{
    public interface IPatientRepository : IBaseRepository<Patient>
    {
        Task<Patient> GetByIdAsync(Guid id);
        Task<IEnumerable<Patient>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
        Task<IEnumerable<Patient>> FindWithNameAsync(string name, int pageNumber, int pageSize);
        Task<IEnumerable<Patient>> FindWithPhoneNumberAsync(string PhoneNumber);
        Task<int> GetTotalCountAsync();
        Task<IEnumerable<Patient>> GetByContactInfo(string info, int pageNumber, int pageSize);
        Task<int> GetCountByContactInfo(string info);
        Task<IEnumerable<Patient>> GetPatientByDateAsync(DateTime dateStart, DateTime dateEnd, int pageNumber, int pageSize);
        Task<int> GetTotalCountByDateAsync(DateTime dateStart, DateTime dateEnd);

        Task<IEnumerable<Patient>> GetPatientsNotExamined(int pageNumber, int pageSize);
        Task<int> GetPatientsNotExaminedCountAsync();

    }
}