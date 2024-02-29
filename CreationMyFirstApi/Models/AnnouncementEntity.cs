using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreationMyFirstApi.Models;
[Table("Announcement", Schema = "dbo")]
public class AnnouncementEntity
{
    [Key]
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public DateTime Date { get; set; }

    public string Description { get; set; } = null!;
}
