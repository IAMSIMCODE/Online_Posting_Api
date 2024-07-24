using Microsoft.EntityFrameworkCore;
using OnlinePosting.Domain.Models;

namespace OnlinePosting.Infrastructure.Data
{
    public class OnlinePostDbContext : DbContext
    {
        public OnlinePostDbContext(DbContextOptions<OnlinePostDbContext> dbContext) : base(dbContext) { }

        public DbSet<PostEntity> PostEntities { get; set; }
        public DbSet<ClientAuth> ClientAuths { get; set; }
    }
}
