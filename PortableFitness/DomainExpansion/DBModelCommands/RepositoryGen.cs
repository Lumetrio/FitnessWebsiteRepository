using Database.Contexts;
using Database.Interfaces___Копировать;
using Database.RepositoryObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DBModelCommands
{
    public class Repository<T> : IBaseRepository<T> where T : class
    {
        private readonly AppDbContext _context;

   
        protected readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }

        public Task CreateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        Task<T> IBaseRepository<T>.UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
