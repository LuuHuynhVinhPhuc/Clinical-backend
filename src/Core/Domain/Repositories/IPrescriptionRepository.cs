﻿using ClinicalBackend.Domain.Entities;
using Domain.Interfaces;

namespace ClinicalBackend.Domain.Repositories
{
    public interface IPrescriptionRepository : IBaseRepository<Prescription>
    {
        Task<IEnumerable<Prescription>> SearchByNameAsync(string Name, int pageNumber, int pageSize);
        Task<IEnumerable<Prescription>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default); 
        Task<Prescription> GetByIdAsync(Guid Id);
        Task<int> GetTotalCountAsync(); 
        Task<int> GetTotalCountByNameAsync(string Name);
        Task<int> GetTotalCountByPatientIdAsync(Guid patientId);
        Task<IEnumerable<Prescription>> GetByPatientIdAsync(Guid patientId, int pageNumber, int pageSize);
        Task<IEnumerable<Prescription>> GetByDateRangeAsync(DateTime dateStart, DateTime dateEnd, int pageNumber, int pageSize);
        Task<int> GetCountByDateRangeAsync(DateTime dateStart, DateTime dateEnd);
        Task<IEnumerable<Prescription>> GetByDateRangeAsync(DateTime dateStart, DateTime dateEnd);
        Task<IEnumerable<Prescription>> GetByFollowUpIdAsync(Guid followUpId);
    }
}
