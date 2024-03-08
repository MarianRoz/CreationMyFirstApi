using CreationMyFirstApi.Models;
using CreationMyFirstApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CreationMyFirstApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AnnouncementController : Controller
    {
        private readonly IAnnouncementService announcementService;
        public AnnouncementController(IAnnouncementService service)
        {
            announcementService = service ??
            throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        public async Task<IEnumerable<AnnouncementEntity?>> Get()
        {
            return await announcementService.Get();
        }
        [HttpGet]
        public async Task<AnnouncementEntity> GetAnnouncementById(int iD)
        {
            return await announcementService.GetAnnouncementById(iD);
        }

        [HttpGet]
        public async Task<IActionResult> Create(AnnouncementEntity ann)
        {
            AnnouncementEntity result = await announcementService.Create(ann);
            if (result.Id == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
            return Ok("Added Announcement");
        }

        [HttpGet]
        public async Task<IActionResult> Update(AnnouncementEntity ann)
        {
            await announcementService.Update(ann);
            return Ok("Updated Announcement");
        }

        [HttpGet]
        public JsonResult Delete(int id)
        {
            announcementService.Delete(id);
            return new JsonResult("Deleted Announcement");
        }
        [HttpGet]
        public async Task<IEnumerable<AnnouncementEntity>> GetSelectedAnnouncementDetails(int id)
        {
            return await announcementService.GetSelectedAnnouncementDetails(id);
        }
    }
}
