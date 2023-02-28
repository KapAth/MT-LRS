using Microsoft.EntityFrameworkCore;

using Repositories.Repository.Entities;

namespace Repositories.Repository
{
    public class MTLRSDbContext : DbContext
    {
        public MTLRSDbContext()
        {
        }

        public MTLRSDbContext(DbContextOptions<MTLRSDbContext> options)
         : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("data source=kapsimalis-nb;initial catalog=MT_LRSdb;trusted_connection=true;TrustServerCertificate=True;");
            }
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserTitle> UserTitle { get; set; }
        public virtual DbSet<UserType> UserType
        {
            get; set;
        }
    }
}
