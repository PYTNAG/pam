using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PAM.Core.Domain;

namespace PAM.Persistence.EntityTypeConfigurations
{
    public class ResidentConfiguration : IEntityTypeConfiguration<Resident>
    {
        public void Configure(EntityTypeBuilder<Resident> builder)
        {
            builder.HasKey(r => r.Id);
            builder.HasIndex(r => r.Id).IsUnique();

            builder.Property(r => r.FirstName).IsRequired();
            builder.Property(r => r.SecondName).IsRequired();

            builder.Property(r => r.Passport).IsRequired();
            builder.Property(r => r.ApartmentId).IsRequired();

            builder
                .HasOne(r => r.Apartment)
                .WithMany(a => a.Residents)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}