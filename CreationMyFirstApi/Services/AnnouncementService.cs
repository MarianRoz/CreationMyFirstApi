using CreationMyFirstApi.Models;
using CreationMyFirstApi.Repository;
using Microsoft.EntityFrameworkCore;

namespace CreationMyFirstApi.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IAnnouncementRepository announcementRepository;
        public int Key { get; internal set; }
        public double Similarity { get; internal set; }

        public AnnouncementService(IAnnouncementRepository context)
        {
            announcementRepository = context ??
                throw new ArgumentNullException(nameof(context));
        }
        public AnnouncementService()
        {
        }
        public async Task<IEnumerable<AnnouncementEntity>> Get()
        {
            using (DbContext connection = new AnnouncementDbContext())
            {
                return await announcementRepository.Get();
            }
        }
        public async Task<AnnouncementEntity> GetAnnouncementById(int iD)
        {
            return await announcementRepository.GetAnnouncementById(iD);
        }
        public async Task<AnnouncementEntity> Create(AnnouncementEntity announcement)
        {
            return await announcementRepository.Create(announcement);
        }
        public async Task<AnnouncementEntity> Update(AnnouncementEntity announcement)
        {
            return await announcementRepository.Update(announcement);
        }
        public bool Delete(int id)
        {
            return announcementRepository.Delete(id);
        }
        public async Task<IList<AnnouncementEntity>> GetSelectedAnnouncementDetails(int id)
        {
            IList<AnnouncementEntity> allAnnouncements = await announcementRepository.All.ToListAsync();
            IList<AnnouncementService> similarityAnnouncements = new List<AnnouncementService>();
            AnnouncementEntity? mainAnnouncement = allAnnouncements.FirstOrDefault(x => x.Id == id);

            List<AnnouncementEntity> result = allAnnouncements
                .Select(x => new
                {
                    x.Date,
                    x.Description,
                    x.Id,
                    x.Title,
                    Similarity = StringComparator.CompareStrings(x.Title, mainAnnouncement.Title)
                })
                .OrderByDescending(x => x.Similarity)
                .Take(3)
                .Select(x => new AnnouncementEntity()
                {
                    Date = x.Date,
                    Description = x.Description,
                    Id = x.Id,
                    Title = x.Title
                })
                .ToList();

            return result;
        }
    }
}