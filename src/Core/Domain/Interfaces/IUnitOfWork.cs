﻿using ClinicalBackend.Domain.Repositories;

namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IRoleRepository Roles { get; }
        IMedicineRepository Medicines { get; }
        IPatientRepository Patient { get; }
        IFollowUpRepository FollowUp { get; }
        IPatientRepository Patient { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}