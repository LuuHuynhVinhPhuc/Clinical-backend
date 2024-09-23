using ClinicalBackend.Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClinicalBackend.Domain.Repositories
{
    public class IPrescriptionRepository : IBaseRepository<Prescription>
    {
        public void Add(Prescription entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(ICollection<Prescription> entities)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Prescription> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Prescription>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Prescription> GetByCondition(Expression<Func<Prescription, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public void Remove(Prescription entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(ICollection<Prescription> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(Prescription entity)
        {
            throw new NotImplementedException();
        }
    }
}
