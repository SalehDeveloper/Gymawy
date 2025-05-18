using Gymawy.Domain.Abstractions;
using Gymawy.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : BaseEntity
    {

        protected readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
           await _context.Set<T>().AddAsync(entity , cancellationToken);
            
            return entity;
        }

        public async Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().CountAsync(cancellationToken);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> criteria, CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().CountAsync(criteria, cancellationToken);
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null, CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
            {
                foreach (string include in includes)
                    query = query.Include(include);
            }
            return await query.Where(criteria).ToListAsync();

        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            if (pageSize > 15)
                pageSize = 15;
            if (pageIndex < 1)
                pageIndex = 1;

            return await _context.Set<T>().Where(criteria).Skip((pageIndex - 1)*pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int? pageIndex, int? pageSize, Expression<Func<T, object>> orderBy = null, string orderByDirection = "ASC", CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _context.Set<T>();

            query = query.Where(criteria);

            if ( (pageIndex.HasValue && pageSize ==null) || (pageSize.HasValue && pageIndex == null))
            {
                pageIndex = 1;
                pageSize = 15;
            }

            if (pageIndex.HasValue)
            {
                if (pageIndex < 1)
                    pageIndex = 1;

                query = query.Skip( (pageIndex.Value-1) *pageSize.Value);
            }

            if (pageSize.HasValue)
            {  
                if (pageSize > 15)
                    pageSize = 15;
                query = query.Take(pageSize.Value);

            }
          
            
            if (orderBy != null)
            {


                query = orderByDirection == OrderBy.Descending
                                 ? query.OrderByDescending(orderBy)
                                 : query.OrderBy(orderBy);
            }

            return await query.ToListAsync(cancellationToken);

        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null, CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _context.Set<T>();

            if ( includes is not null && includes.Count() > 0 )
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
           
            
            return await query.SingleOrDefaultAsync(criteria , cancellationToken);

           

             
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAllAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            if (pageSize>15) 
                pageSize = 15;
            if(pageIndex <1)
                pageIndex = 1;


           return await  _context.Set<T>().Skip((pageIndex-1)*pageSize ).Take(pageSize).ToListAsync(cancellationToken); 
        }

        public async Task<T> GetByIdAsync(Guid Id, CancellationToken cancellationToken = default)
        {
             return await _context.Set<T>().FindAsync(Id, cancellationToken); 
        }

        public Task Update(T entity)
        {
           _context.Set<T>().Update(entity);

            return Task.CompletedTask;
        }

        public IEnumerable<T> UpdateRange(IEnumerable<T> entities)
        {
           _context.Set<T>().UpdateRange(entities);

            return entities;
        }
    }
}
