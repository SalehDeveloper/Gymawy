using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Domain.Abstractions
{
    public  interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);

        Task<T> GetByIdAsync(Guid Id, CancellationToken cancellationToken = default);

        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<IEnumerable<T>> GetAllAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default);

        Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T,
        bool>> criteria,
        string[] includes = null,
        CancellationToken cancellationToken = default);

        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria,
          int take,
          int skip,
          CancellationToken cancellationToken = default);

        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria,
            int? skip,
            int? take,
          Expression<Func<T, object>> orderBy = null,
          string orderByDirection = OrderBy.Ascending,
          CancellationToken cancellationToken = default);

        Task Update(T entity);

        IEnumerable<T> UpdateRange(IEnumerable<T> entities);

        Task<int> CountAsync(CancellationToken cancellationToken = default);

        Task<int> CountAsync(Expression<Func<T, bool>> criteria, CancellationToken cancellationToken = default);
    }
}
