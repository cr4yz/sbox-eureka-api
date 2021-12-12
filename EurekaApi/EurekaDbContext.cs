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

        public DbSet<ForumItem> ForumItems { get; set; } = null!;

    }
}
