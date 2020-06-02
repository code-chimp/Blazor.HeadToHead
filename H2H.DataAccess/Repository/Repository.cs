using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using H2H.DataAccess.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace H2H.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Context;
        internal readonly DbSet<T> DbSet;

        public Repository(DbContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        private IQueryable<T> FormatQuery(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                var properties = includeProperties.Split(
                    new[] {','},
                    StringSplitOptions.RemoveEmptyEntries
                );

                query = properties.Aggregate(
                    query,
                    (current, property) => current.Include(property));
            }

            return query.AsNoTracking();
        }

        public T Get(int id)
        {
            return DbSet.Find(id);
        }

        public async Task<T> GetAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null)
        {
            var query = FormatQuery(filter, includeProperties);

            return orderBy == null ? query.ToList() : orderBy(query).ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null)
        {
            var query = FormatQuery(filter, includeProperties);

            return orderBy == null ? await query.ToListAsync() : await orderBy(query).ToListAsync();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            var query = FormatQuery(filter, includeProperties);

            return query.FirstOrDefault();
        }

        public async Task<T> GetFirstOrDefaultAsync(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null)
        {
            var query = FormatQuery(filter, includeProperties);

            return await query.FirstOrDefaultAsync();
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public async Task AddAsync(T entity)
        {
            await DbSet.AddAsync(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            DbSet.AddRange(entities);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await DbSet.AddRangeAsync(entities);
        }

        public void Remove(int id)
        {
            Remove(DbSet.Find(id));
        }

        public async Task RemoveAsync(int id)
        {
            var entity = await GetAsync(id);
            DbSet.Remove(entity);
        }

        public void Remove(T entity)
        {
            DbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            DbSet.RemoveRange(entities);
        }
    }
}
