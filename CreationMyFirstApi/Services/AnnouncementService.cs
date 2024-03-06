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

            IList<AnnouncementEntity> allAnnouncements = new List<AnnouncementEntity>();
            foreach (AnnouncementEntity item in await Get())
                allAnnouncements.Add(item);

            IList<AnnouncementService> similarityAnnouncements = new List<AnnouncementService>();

            foreach (AnnouncementEntity item in allAnnouncements)
                similarityAnnouncements.Add(new AnnouncementService { Key = item.Id, Similarity = StringComparator.CompareStrings(item.Title, GetAnnouncementById(id).Result.Title) });
            AnnouncementService[] res = similarityAnnouncements.OrderByDescending(x => x.Similarity).Where(x => x.Key != id).Take(3).ToArray();

            IList<AnnouncementEntity> TopAnnouncement = new List<AnnouncementEntity>();
            TopAnnouncement.Add(await GetAnnouncementById(id));
            foreach (AnnouncementEntity item in allAnnouncements.Where(x => x.Id == res[0].Key || x.Id == res[1].Key || x.Id == res[2].Key))
                TopAnnouncement.Add(item);

            return TopAnnouncement;
        }
    }
}