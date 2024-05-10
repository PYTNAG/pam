using Microsoft.EntityFrameworkCore;
using PAM.Core.App.Validation;
using PAM.Core.Domain;
using PAM.Persistence.EntityTypeConfigurations;

namespace PAM.Persistence
{
    public sealed class PersonalAccountsDbContext : DbContext, IPersonalAccountsDbContext
    {
        public DbSet<PersonalAccount> PersonalAccounts { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Resident> Residents { get; set; }

        public PersonalAccountsDbContext(DbContextOptions<PersonalAccountsDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonalAccountConfiguration());
            modelBuilder.ApplyConfiguration(new ApartmentConfiguration());
            modelBuilder.ApplyConfiguration(new ResidentConfiguration());
            
            base.OnModelCreating(modelBuilder);
        }
    }
}