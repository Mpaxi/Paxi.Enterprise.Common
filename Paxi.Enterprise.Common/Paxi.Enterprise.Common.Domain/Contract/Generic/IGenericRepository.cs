using Paxi.Enterprise.Common.Domain.Contract.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Paxi.Enterprise.Common.Domain.Contract.Generic
{
    public interface IGenericRepository<TEntity> where TEntity : IEntityBase
    {
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<ICollection<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        TEntity Get(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        TEntity Add(TEntity Entity);
        Task<TEntity> AddAsync(TEntity Entity);
        void AddRange(IEnumerable<TEntity> Entity);
        Task AddRangeAsync(IEnumerable<TEntity> Entity);
        TEntity Update(TEntity Entity);
        void UpdateRange(IEnumerable<TEntity> Entity);
        TEntity Delete(TEntity Entity);
        void DeleteRange(IEnumerable<TEntity> Entity);
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
