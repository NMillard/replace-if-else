using Medium.ReplacingIfElse.Domain;
using Microsoft.EntityFrameworkCore;

namespace Medium.ReplacingIfElse.DataLayer {
    internal class DataStore : DbContext {
        public DataStore(DbContextOptions options) : base(options) {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.HasDefaultSchema("Application");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataStore).Assembly);
        }
    }
}