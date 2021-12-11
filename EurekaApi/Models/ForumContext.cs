using Microsoft.EntityFrameworkCore;

namespace EurekaApi.Models
{
    public class ForumContext : DbContext
    {

        public ForumContext(DbContextOptions<ForumContext> options)
            : base(options)
        {

        }

        public DbSet<ForumItem> ForumItems { get; set; } = null!;

    }
}
