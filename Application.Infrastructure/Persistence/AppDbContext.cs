using Application.Core.ProjectAggregate;
using Microsoft.EntityFrameworkCore;

namespace Application.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        { }

        public AppDbContext()
        { }

        //DbSets Goes Here
        public DbSet<Person> Persons { get; set; }
        public DbSet<Address> Addresses { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
