using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Paxi.Enterprise.Common.Domain.Contract.Base;
using Paxi.Enterprise.Common.Domain.Contract.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Paxi.Enterprise.Common.Infra.Database.Generic
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntityBase
    {
        private readonly DbContext _context;
        public GenericRepository(DbContext context)
        {
            _context = context;
        }

        public TEntity Add(TEntity Entity)
        {
            return _context.Set<TEntity>().Add(Entity).Entity;
        }

        public async Task<TEntity> AddAsync(TEntity Entity)
        {
            EntityEntry<TEntity> EntityReturn = await _context.Set<TEntity>().AddAsync(Entity);
            //await _context.SaveChangesAsync();
            return EntityReturn.Entity;
        }

        public void AddRange(IEnumerable<TEntity> Entity)
        {
            _context.Set<TEntity>().AddRange(Entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> Entity)
        {
            await _context.Set<TEntity>().AddRangeAsync(Entity);
        }

        public TEntity Delete(TEntity Entity)
        {
            Entity.Active = false;
            EntityEntry<TEntity> EntityReturn = _context.Set<TEntity>().Update(Entity);
            //_context.SaveChanges();
            return EntityReturn.Entity;
        }

        public void DeleteRange(IEnumerable<TEntity> Entity)
        {
            Entity.All(e => { e.Active = false; return true; });
            _context.Set<TEntity>().UpdateRange(Entity);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> queryable = _context.Set<TEntity>();

            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include(includeProperty);
            }
            return queryable.FirstOrDefault(predicate);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> queryable = _context.Set<TEntity>();

            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include(includeProperty);
            }
            return await queryable.FirstOrDefaultAsync(predicate).ConfigureAwait(false);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public TEntity Update(TEntity Entity)
        {
            EntityEntry<TEntity> EntityReturn = _context.Set<TEntity>().Update(Entity);
            //_context.SaveChanges();
            return EntityReturn.Entity;
        }

        public void UpdateRange(IEnumerable<TEntity> Entity)
        {
            _context.Set<TEntity>().UpdateRange(Entity);
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> queryable = _context.Set<TEntity>();

            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include(includeProperty);
            }

            return queryable.Where(predicate);
        }

        public async Task<ICollection<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> queryable = _context.Set<TEntity>();

            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include(includeProperty);
            }

            return await queryable.Where(predicate).ToListAsync().ConfigureAwait(false);
        }
    }
}
