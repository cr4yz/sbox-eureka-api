using Microsoft.EntityFrameworkCore;
using EurekaApi.Models;

namespace EurekaApi
{
    public class EurekaDbContext : DbContext
    {

        public EurekaDbContext(DbContextOptions<EurekaDbContext> options)
            : base(options)
        {

        }

        public DbSet<Forum> Forums { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var forums = new List<Forum>()
            {
                    new Forum()
                    {
                        Id = 1,
                        Name = "General",
                        Description = "A nice spot to talk about anything"
                    },
                    new Forum()
                    {
                        Id = 2,
                        Name = "Games",
                        Description = "Talk about games"
                    }
            };

            builder.Entity<Forum>().HasData(forums);
        }

    }
}
