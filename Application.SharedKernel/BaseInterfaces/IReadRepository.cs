using System.Linq.Expressions;
using EntityBase = Application.SharedKernel.BaseEntity.BaseEntity;

namespace Application.SharedKernel.BaseInterfaces
{
    public  interface IReadRepository<T>where T : EntityBase
    {
        Task<T> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull;

        Task<List<T>> ListAsync(CancellationToken cancellationToken = default);

        IQueryable<T> WhereAsync(Expression<Func<T, bool>> filter);

        Task<T> FirstOrDefaultAsync(CancellationToken cancellationToken = default);
    }
}
