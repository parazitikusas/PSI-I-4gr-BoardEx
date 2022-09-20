using BoardEx.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BoardEx.Web.Data
{
    public class BoardExDbContext : DbContext
    {
        public BoardExDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BoardAd> BoardAds { get; set; }

        public DbSet<Tag> Tags { get; set; }
    }
}
