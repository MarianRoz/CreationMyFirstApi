namespace CreationMyFirstApi.Models
{
    public class AnnouncementDetails
    {
        public required AnnouncementDetails SelectedAnnouncement { get; set; }
        public required IList<AnnouncementDetails> TopSimilar { get; set; }
    }
}
