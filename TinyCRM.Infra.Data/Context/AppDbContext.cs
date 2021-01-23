using Microsoft.EntityFrameworkCore;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Infra.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<NaturalPerson> NaturalPeople { get; set; }
        public DbSet<LegalPerson> LegalPeople { get; set; }

        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Person>()
                .HasOne(p => p.Address)
                .WithOne(a => a.Person)
                .HasForeignKey<Address>(a => a.PersonId);
        }
    }
}
