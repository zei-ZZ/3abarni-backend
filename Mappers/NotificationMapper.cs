using _3abarni_backend.DTOs;
using _3abarni_backend.Models;

namespace _3abarni_backend.Mappers
{
    public static class NotificationMapper
    {
        public static NotificationDto MapToDto(Notification notification)
        {
            return new NotificationDto
            {
                Id = notification.Id,
                Content = notification.Content,
                IsRead = notification.IsRead,
                UserId = notification.UserId
            };
        }
    }
}
