using System.Linq.Expressions;

namespace Repositories.RepositoryBase
{
    public interface IRepositoryBase<TEntity, TId> where TEntity : class
    {
        TEntity GetById(TId id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetByFilter(Expression<Func<TEntity, bool>> filter);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
    }
}
