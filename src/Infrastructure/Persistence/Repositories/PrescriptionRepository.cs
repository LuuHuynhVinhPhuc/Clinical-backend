using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Domain.Repositories;
using ClinicalBackend.Persistence.Context;
using Domain.Common;

namespace ClinicalBackend.Persistence.Repositories
{
    public class PrescriptionRepository : BaseRepository<Prescription>, IPrescriptionRepository
    {
        public PrescriptionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}