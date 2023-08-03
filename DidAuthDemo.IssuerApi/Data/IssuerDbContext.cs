using DidAuthDemo.IssuerApi.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DidAuthDemo.IssuerApi.Data
{
    public class IssuerDbContext : DbContext
    {
        public DbSet<Auth> Auths { get; set; } = default!;
        public DbSet<CredentialSchema> CredentialSchemas { get; set; } = default!;

        public IssuerDbContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSnakeCaseNamingConvention();
        }
    }
}
