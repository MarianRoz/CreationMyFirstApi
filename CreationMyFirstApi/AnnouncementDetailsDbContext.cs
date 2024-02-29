using CreationMyFirstApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CreationMyFirstApi
{
    public class AnnouncementDetailsDbContext : DbContext
    {
        public AnnouncementDetailsDbContext(DbContextOptions<AnnouncementDetailsDbContext> options)
        : base(options)
        {
        }

        public DbSet<AnnouncementDetails> AnnouncementsDetails { get; set; }
    }
}
