using CreationMyFirstApi.Models;

namespace CreationMyFirstApi.Repository
{
    public interface IAnnouncementRepository
    {
        Task<AnnouncementEntity> GetAnnouncementById(int iD);
        Task<IEnumerable<AnnouncementEntity>> Get();
        Task<AnnouncementEntity> Create(AnnouncementEntity announcement);
        Task<AnnouncementEntity> Update(AnnouncementEntity announcement);
        bool Delete(int ID);
        Task<IEnumerable<AnnouncementEntity>> GetSelectedAnnouncementDetails(IList<AnnouncementEntity> id);
    }
}