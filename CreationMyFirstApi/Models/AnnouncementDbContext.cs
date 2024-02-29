using Microsoft.EntityFrameworkCore;

namespace CreationMyFirstApi.Models;

public class AnnouncementDbContext : DbContext
{
    public AnnouncementDbContext()
    {
    }
    public AnnouncementDbContext(DbContextOptions<AnnouncementDbContext> options) : base(options) { }

    public DbSet<AnnouncementEntity> Announcements { get; set; }
}

