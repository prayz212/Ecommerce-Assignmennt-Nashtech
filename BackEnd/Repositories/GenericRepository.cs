using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BackEnd.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BackEnd.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected ApplicationDbContext _context;
        internal DbSet<T> _dbSet;
        public readonly ILogger _logger;

        public GenericRepository(ApplicationDbContext context, ILogger logger)
        {
            _context = context;
            _dbSet = context.Set<T>();
            _logger = logger;
        }

        public virtual async Task<IEnumerable<T>> GetAll (
            Expression<Func<T, bool>> filter = null,
            int page = 0, 
            int size = 0, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includes = ""
        )
        {
            try
            {
                IQueryable<T> query = _dbSet;
                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (page != 0 && size != 0)
                {
                    var skip = GetSkipRecord(page, size);
                    query = query
                        .Skip(skip)
                        .Take(size);
                }

                foreach (var include in includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(include);
                }

                if (orderBy != null)
                {
                    return await orderBy(query).ToListAsync();
                }
                else
                {
                    return await query.ToListAsync();
                }
            } 
            catch (Exception e)
            {
                _logger.LogError(e, $"{typeof(GenericRepository<T>)} GetAll function error");
                return new List<T>();
            }
        }

        public virtual async Task<T> GetBy(Expression<Func<T, bool>> predicate, string includes = "")
        {
            try
            {
                IQueryable<T> query = _dbSet;
                foreach (var include in includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(include);
                }

                return await query.FirstOrDefaultAsync(predicate);
            } 
            catch (Exception e)
            {
                _logger.LogError(e, $"{typeof(GenericRepository<T>)} GetBy function error");
                return null;
            }
        }

        public virtual async Task<bool> Add(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                return true;
            } 
            catch (Exception e)
            {
                _logger.LogError(e, $"{typeof(GenericRepository<T>)} Add function error");
                return false;
            }
        }

        public virtual async Task<bool> AddRange(IEnumerable<T> entities)
        {
            try
            {
                await _dbSet.AddRangeAsync(entities);
                return true;
            } 
            catch (Exception e)
            {
                _logger.LogError(e, $"{typeof(GenericRepository<T>)} AddRange function error");
                return false;
            }
        }

        public virtual bool Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual bool DeleteRange(IEnumerable<T> entites)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> Update(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<int> CountAll(Expression<Func<T, bool>> filter = null)
        {
            try
            {
                IQueryable<T> query = _dbSet;
                if (filter != null)
                {
                    query = query.Where(filter);
                }

                return await query.CountAsync();
            } 
            catch (Exception e)
            {
                _logger.LogError(e, $"{typeof(GenericRepository<T>)} CountAll function error");
                return 0;
            }
        }

        protected int GetSkipRecord(int page, int size)
        {
            return (page - 1) * size;
        }
    }
}