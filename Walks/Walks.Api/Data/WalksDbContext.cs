using Microsoft.EntityFrameworkCore;
using Walks.Api.Models.Domain;

namespace Walks.Api.Data
{
    public class WalksDbContext : DbContext
    {
        public WalksDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

    }
}
