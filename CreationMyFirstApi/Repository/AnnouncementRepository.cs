using CreationMyFirstApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CreationMyFirstApi.Repository
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly AnnouncementDbContext announcementDbContext;
        public AnnouncementRepository(AnnouncementDbContext context)
        {
            announcementDbContext = context ??
                throw new ArgumentNullException(nameof(context));
        }
        public IQueryable<AnnouncementEntity> All => announcementDbContext.Set<AnnouncementEntity>();
        public AnnouncementRepository()
        {
        }
        public async Task<IEnumerable<AnnouncementEntity>> Get()
        {
            return await announcementDbContext.Announcements.ToListAsync();
        }
        public async Task<AnnouncementEntity> GetAnnouncementById(int iD)
        {
            using (DbContext connection = new AnnouncementDbContext())
            {
                return await announcementDbContext.Announcements.FindAsync(iD);
            }
        }
        public async Task<AnnouncementEntity> Create(AnnouncementEntity announcement)
        {
            announcementDbContext.Announcements.Add(announcement);
            await announcementDbContext.SaveChangesAsync();
            return announcement;
        }
        public async Task<AnnouncementEntity> Update(AnnouncementEntity announcement)
        {
            announcementDbContext.Entry(announcement).State = EntityState.Modified;
            await announcementDbContext.SaveChangesAsync();
            return announcement;
        }
        public bool Delete(int ID)
        {
            bool result = false;
            AnnouncementEntity? department = announcementDbContext.Announcements.Find(ID);
            if (department != null)
            {
                announcementDbContext.Entry(department).State = EntityState.Deleted;
                announcementDbContext.SaveChanges();
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
        public async Task<IEnumerable<AnnouncementEntity>> GetSelectedAnnouncementDetails(IList<AnnouncementEntity> id)
        {
            return await announcementDbContext.Announcements.ToListAsync();
        }
    }
}