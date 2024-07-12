using System;
using Data.DAL;
using Data.Repositories.Interfaces;
using Domain.Commons;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DataContext _context;
        private readonly DbSet<T> _table;
        public Repository(DataContext context)
        {
            _context = context;
            _table = context.Set<T>();
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Create(T entity)
        {
            entity.CreatedAt = DateTime.Now;
            entity.RegUser = "System";
            var resoult = _context.Entry(entity);
            resoult.State = EntityState.Added;
        }

        public async Task Delete(T entity)
        {
            var resoult = _context.Entry(entity);
            resoult.State = EntityState.Deleted;
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> predicate = null, params string[] includes)
        {
            IQueryable<T> query = _table;

            if (includes.Length > 0)
            {
                query = GetAllIncludes(includes);
            }
            return predicate == null ? await query.OrderByDescending(e => e.CreatedAt).ToListAsync() : await query.Where(predicate).OrderByDescending(e => e.CreatedAt).ToListAsync();
        }

        public async Task<T> GetEntity(Expression<Func<T, bool>> predicate = null, params string[] includes)
        {
            IQueryable<T> query = _table;
            if (includes.Length > 0)
            {
                query = GetAllIncludes(includes);
            }
            return predicate == null ? await query.FirstOrDefaultAsync() : await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<bool> IsExist(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null ? false : await _table.AnyAsync(predicate);
        }

        public async Task Update(T entity)
        {
            var resoult = _context.Entry(entity);
            resoult.State = EntityState.Modified;
        }
        public IQueryable<T> GetAllIncludes(params string[] includes)
        {
            IQueryable<T> query = _table;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query;
        }
    }
}

