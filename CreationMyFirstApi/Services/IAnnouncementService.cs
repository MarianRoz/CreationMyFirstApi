using CreationMyFirstApi.Models;

namespace CreationMyFirstApi.Services
{
    public interface IAnnouncementService
    {
        Task<AnnouncementEntity> GetAnnouncementById(int iD);
        Task<IEnumerable<AnnouncementEntity>> Get();
        Task<AnnouncementEntity> Create(AnnouncementEntity announcement);
        Task<AnnouncementEntity> Update(AnnouncementEntity announcement);
        bool Delete(int id);
        Task<IList<AnnouncementEntity>> GetSelectedAnnouncementDetails(int id);

    }
}