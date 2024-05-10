using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PAM.Core.Domain;

namespace PAM.Persistence.EntityTypeConfigurations
{
    public class ApartmentConfiguration : IEntityTypeConfiguration<Apartment>
    {
        public void Configure(EntityTypeBuilder<Apartment> builder)
        {
            builder.HasKey(a => a.Id);
            builder.HasIndex(a => a.Id).IsUnique();

            builder.Property(a => a.Address).IsRequired();
            builder.HasIndex(a => a.Address).IsUnique();
            
            builder.Property(a => a.Area).IsRequired();
        }
    }
}