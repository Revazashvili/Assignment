using Application.Core.Interface;
using Application.Core.ProjectAggregate;
using Application.Infrastructure.Persistence;
using Application.SharedKernel;
using System.Data.Entity;

namespace Application.Infrastructure.Repository
{
    public class PersonRepository : RepositoryBase<Person>, IPersonRepository
    {
        private readonly AppDbContext _dbContext;
        public PersonRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Person> ListWithDependentObjectsAsync()
        {
            return _dbContext.Persons.Include(x => x.Address).ToList();
        }
    }
}
