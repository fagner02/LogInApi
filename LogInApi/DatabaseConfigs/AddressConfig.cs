using LogInApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LogInApi.Configs {
    public class AddressConfg : IEntityTypeConfiguration<Address> {
        public void Configure(EntityTypeBuilder<Address> builder) {
            builder.Property(x => x.Number).HasMaxLength(5);
            builder.Property(x => x.State).HasMaxLength(20);
            builder.Property(x => x.Street).HasMaxLength(40);
            builder.Property(x => x.City).HasMaxLength(20);
            builder.Property(x => x.District).HasMaxLength(20);
        }
    }
}