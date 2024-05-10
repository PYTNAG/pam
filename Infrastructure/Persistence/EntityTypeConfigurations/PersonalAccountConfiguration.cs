using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PAM.Core.Domain;

namespace PAM.Persistence.EntityTypeConfigurations
{
    public class PersonalAccountConfiguration : IEntityTypeConfiguration<PersonalAccount>
    {
        public void Configure(EntityTypeBuilder<PersonalAccount> builder)
        {
            builder.HasKey(pa => pa.Id);
            builder.HasIndex(pa => pa.Id).IsUnique();

            builder.Property(pa => pa.Number).IsRequired();
            builder.HasIndex(pa => pa.Number).IsUnique();
            
            builder.Property(pa => pa.BeginAt).IsRequired();
            builder.Property(pa => pa.EndAt).IsRequired();
            builder.Property(pa => pa.ApartmentId).IsRequired();

            builder
                .HasOne(pa => pa.Apartment)
                .WithMany(a => a.PersonalAccounts)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}