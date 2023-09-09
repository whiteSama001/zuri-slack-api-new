using Microsoft.EntityFrameworkCore;
using slack_api_1.Models;

namespace slack_api_1.Data
{
    public class SlackContext : DbContext
    {
        public SlackContext(DbContextOptions<SlackContext> options) : base(options)
        {
        }

        public DbSet<slackModel> slackData { get; set; }

        // Add DbSet properties for other entities if needed

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure entity relationships, indexes, and other database-specific settings here

            // Example: modelBuilder.Entity<EntityName>().HasKey(e => e.Id);
        }
    }
}
