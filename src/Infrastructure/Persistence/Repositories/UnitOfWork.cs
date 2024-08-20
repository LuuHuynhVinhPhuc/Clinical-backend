using ClinicalBackend.Domain.Repositories;
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
        public IMedicineRepository Medicines { get; }
        public IPatientInfoRepository PatientInfo { get; }

        public UnitOfWork(ApplicationDbContext context, IUserRepository users, IRoleRepository roles, IMedicineRepository medicines, IPatientInfoRepository patientInfo)
        {
            _context = context;
            Users = users;
            Roles = roles;
            Medicines = medicines;
            PatientInfo = patientInfo;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync();
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
