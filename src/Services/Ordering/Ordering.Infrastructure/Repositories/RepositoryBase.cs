using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Filters;
using Ordering.Domain.Common;
using Ordering.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    public abstract class RepositoryBase<T> : IAsyncRepository<T> where T : EntityBase
    {
        protected readonly OrderContext _dbContext;

        public RepositoryBase(OrderContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync() => 
            await _dbContext.Set<T>().ToListAsync();
        
        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate) => 
            await _dbContext.Set<T>().Where(predicate).ToListAsync();
        
        public async Task<IReadOnlyList<T>> GetAsync(FilterBase<T> filter, string includeString)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            return await GetAsync(query, filter, () => IncludeByString(query, includeString));
        }

        public async Task<IReadOnlyList<T>> GetAsync(FilterBase<T> filter, IList<Expression<Func<T, object>>> includes)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            return await GetAsync(query, filter, () => IncludeByExpression(query, includes));
        }

        public virtual async Task<T> GetByIdAsync(int id) => await _dbContext.Set<T>().FindAsync(id);

        public async Task<T> AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);

            await _dbContext.SaveChangesAsync();
            
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);

            await _dbContext.SaveChangesAsync();
        }

        #region [PRIVATE METHODS]
        private async Task<IReadOnlyList<T>> GetAsync(IQueryable<T> query, FilterBase<T> filter, Action action)
        {
            query = query.Where(filter.Predicate);

            if (filter.DisableTracking)
                query = query.AsNoTracking();

            action.Invoke();

            if (filter.OrderBy is not null)
                return await filter.OrderBy(query).ToListAsync();

            return await query.ToListAsync();
        }

        private void IncludeByExpression(IQueryable<T> query, IList<Expression<Func<T, object>>> includes)
        {
            if (includes is not null)
                query = includes.Aggregate(query, (current, include) => current.Include(include));
        }

        private void IncludeByString(IQueryable<T> query, string includes)
        {
            if (!string.IsNullOrWhiteSpace(includes))
                query = query.Include(includes);
        }

        #endregion
    }
}
