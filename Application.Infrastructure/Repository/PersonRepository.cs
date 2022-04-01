using Application.Core.Interface;
using Application.Core.ProjectAggregate;
using Application.Infrastructure.Persistence;
using Application.SharedKernel;

namespace Application.Infrastructure.Repository
{
    public class PersonRepository : RepositoryBase<Person>, IPersonRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IAddressRepository _addRessRepository;

        public PersonRepository(AppDbContext dbContext, IAddressRepository addressRepository) : base(dbContext)
        {
            _dbContext = dbContext;
            _addRessRepository = addressRepository;
        }

        //Include Does not worked here, so I did not spent many time on fixing that.
        public async Task<List<Person>> ListWithDependentObjectsAsync(CancellationToken cancellationToken)
        {
            var persons = await ListAsync(cancellationToken);

            foreach (var person in persons)
            {
#pragma warning disable CS8714 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'notnull' constraint.
                var address = await _addRessRepository.GetByIdAsync(person.AddressId, cancellationToken);
#pragma warning restore CS8714 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'notnull' constraint.

                if (address != null)
                    person.Address = address;
            }

            return persons;
        }
    }
}
