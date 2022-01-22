using LogInApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LogInApi.Configs {
    class CollaboratorConfig : IEntityTypeConfiguration<Collaborator> {
        public void Configure(EntityTypeBuilder<Collaborator> builder) {
            builder.Property(x => x.Cpf).IsRequired(true).HasMaxLength(14);
            builder.HasKey(x => x.Cpf);
            builder.Property(x => x.FullName).IsRequired(true).HasMaxLength(100);
            builder.Property(x => x.AddressId).IsRequired(false);
            builder.Property(x => x.Sex).HasMaxLength(1);
            builder.Property(x => x.Phone).HasMaxLength(20);
            builder
                .HasOne(x => x.Address)
                .WithMany(x => x.Collaborators)
                .HasForeignKey(x => x.AddressId)
                .HasPrincipalKey(x => x.Id)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}