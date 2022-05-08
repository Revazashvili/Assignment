using Application.Core.ProjectAggregate;
using Microsoft.EntityFrameworkCore;

namespace Application.Core.Interfaces;

public interface IAppDbContext : IDisposable
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Address> Addresses { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}