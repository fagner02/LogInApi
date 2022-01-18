using LogInApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LogInApi.Contexts {
    public class DatabaseContext : DbContext {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
        }
        public DbSet<Collaborator> Clients { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}