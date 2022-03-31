using Application.Core.Interface;
using Application.Core.ProjectAggregate;
using Application.Infrastructure.Persistence;
using Application.SharedKernel;

namespace Application.Infrastructure.Repository
{
    public class AddressRepository : RepositoryBase<Address>, IAddressRepository
    {
        public AddressRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
