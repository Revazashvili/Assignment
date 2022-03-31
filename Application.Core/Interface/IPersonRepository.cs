using Application.Core.ProjectAggregate;
using Application.SharedKernel.BaseInterfaces;
using System.Linq.Expressions;

namespace Application.Core.Interface
{
    public interface IPersonRepository : IRepositoryBase<Person>
    {
        List<Person> ListWithDependentObjectsAsync();
    }
}
