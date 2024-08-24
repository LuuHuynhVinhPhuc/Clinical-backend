﻿using ClinicalBackend.Domain.Repositories;

namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IRoleRepository Roles { get; }
        IMedicineRepository Medicines { get; }
        IPatientInfoRepository PatientInfo { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}