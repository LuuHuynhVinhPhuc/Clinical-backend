using System.Linq.Expressions;

namespace Domain.Interfaces
{
    public interface IBaseRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);

        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> GetByCondition(Expression<Func<TEntity, bool>> expression);

        void Add(TEntity entity);

        void AddRange(ICollection<TEntity> entities);

        void Update(TEntity entity);

        void Remove(TEntity entity);

        void RemoveRange(ICollection<TEntity> entities);
    }
}