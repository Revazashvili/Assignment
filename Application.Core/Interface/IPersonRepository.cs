using Application.Core.ProjectAggregate;
using Application.SharedKernel.BaseInterfaces;

namespace Application.Core.Interface
{
    public interface IPersonRepository : IRepositoryBase<Person>
    {
        Task<List<Person>> ListWithDependentObjectsAsync(CancellationToken cancellationToken);
    }
}
