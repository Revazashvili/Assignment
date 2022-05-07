using System.Reflection;
using Application.Core.Interfaces;
using Application.Core.ProjectAggregate;
using Microsoft.EntityFrameworkCore;

namespace Application.Infrastructure.Persistence;

public class AppDbContext : DbContext,IAppDbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Person> Persons { get; set; }
    public DbSet<Address> Addresses { get; set; }
        
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

}