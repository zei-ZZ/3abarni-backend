using System.Collections.Generic;
using _3abarni_backend.DTOs;
using _3abarni_backend.Mappers;
using _3abarni_backend.Models;
using _3abarni_backend.Repositories;

namespace _3abarni_backend.Services
{
    public class NotificationService
    {
        private readonly NotificationRepository _notificationRepository;

        public NotificationService(NotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public IEnumerable<NotificationDto> GetAll()
        {
            var notifications = _notificationRepository.GetAll();
            return notifications.Select(NotificationMapper.MapToDto);
        }

        public NotificationDto GetById(int id)
        {
            var notification = _notificationRepository.GetById(id);
            return notification != null ? NotificationMapper.MapToDto(notification) : null;
        }

        public void Create(NotificationDto notificationDto)
        {
            var notification = new Notification
            {
                Content = notificationDto.Content,
                IsRead = notificationDto.IsRead,
                UserId = notificationDto.UserId
            };
            _notificationRepository.Create(notification);
        }

        public void Update(int id, NotificationDto notificationDto)
        {
            var existingNotification = _notificationRepository.GetById(id);
            if (existingNotification != null)
            {
                existingNotification.Content = notificationDto.Content;
                existingNotification.IsRead = notificationDto.IsRead;
                existingNotification.UserId = notificationDto.UserId;
                _notificationRepository.Update(existingNotification);
            }
        }

        public void Delete(int id)
        {
            _notificationRepository.Delete(id);
        }
    }
}
