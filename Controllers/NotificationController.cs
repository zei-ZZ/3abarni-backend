using Microsoft.AspNetCore.Mvc;
using _3abarni_backend.DTOs;
using _3abarni_backend.Services;

namespace _3abarni_backend.Controllers
{
    [Route("notifications")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationService _notificationService;

        public NotificationController(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        public IActionResult GetAllNotifications()
        {
            var notifications = _notificationService.GetAll();
            return Ok(notifications);
        }

        [HttpGet("{id}")]
        public IActionResult GetNotificationById(int id)
        {
            var notification = _notificationService.GetById(id);
            if (notification == null)
            {
                return NotFound();
            }
            return Ok(notification);
        }

        [HttpPost]
        public IActionResult CreateNotification([FromBody] NotificationDto notificationDto)
        {
            _notificationService.Create(notificationDto);
            return CreatedAtAction(nameof(GetNotificationById), new { id = notificationDto.Id }, notificationDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateNotification(int id, [FromBody] NotificationDto notificationDto)
        {
            _notificationService.Update(id, notificationDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteNotification(int id)
        {
            _notificationService.Delete(id);
            return NoContent();
        }
    }
}
