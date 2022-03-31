using EntityBase = Application.SharedKernel.BaseEntity.BaseEntity;

namespace Application.SharedKernel.BaseInterfaces
{
    public  interface IRepositoryBase<T> : IReadRepository<T> where T : EntityBase
    {
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);

        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);

        Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
