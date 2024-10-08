﻿using ClinicalBackend.Domain.Repositories;
using ClinicalBackend.Persistence.Context;
using Domain.Interfaces;

namespace ClinicalBackend.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        public IUserRepository Users { get; }
        public IRoleRepository Roles { get; }
        public IMedicineRepository Medicines { get; }
        public IPatientRepository Patient { get; }
        public IFollowUpRepository FollowUp { get; }
        public IPrescriptionRepository Prescription { get; }

        public UnitOfWork(ApplicationDbContext context, IUserRepository users, IRoleRepository roles, IMedicineRepository medicines, IPatientRepository patientInfo, IFollowUpRepository followUp, IPrescriptionRepository prescription)
        {
            _context = context;
            Users = users;
            Roles = roles;
            Medicines = medicines;
            Patient = patientInfo;
            FollowUp = followUp;
            Prescription = prescription;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}