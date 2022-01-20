using LogInApi.Configs;
using LogInApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LogInApi.Contexts {
    public class DatabaseContext : DbContext {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
            builder.Entity<Collaborator>(new CollaboratorConfig().Configure);
            builder.Entity<Address>(new AddressConfg().Configure);
        }
        public DbSet<Collaborator> Collaborators { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}