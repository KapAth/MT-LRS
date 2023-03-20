using Microsoft.EntityFrameworkCore;
using WebAPI.Repositories.Repository.Entities;

namespace WebAPI.Repositories.Repository
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

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserTitle> UserTitle { get; set; }
        public virtual DbSet<UserType> UserType { get; set; }
    }
}