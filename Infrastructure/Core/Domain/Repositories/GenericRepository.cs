using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected DbContext _dbContext { get; set; }

        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public virtual Task<IQueryable<T>> GetAll()
        {
            return Task.FromResult(_dbContext.Set<T>().AsNoTracking());
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task CreateAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }
    }
}
