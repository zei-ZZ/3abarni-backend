using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using _3abarni_backend.Models;

namespace _3abarni_backend.Repositories
{
    public class NotificationRepository
    {
        private readonly AppDbContext _dbContext;

        public NotificationRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Notification> GetAll()
        {
            return _dbContext.Notifications.ToList();
        }

        public Notification GetById(int id)
        {
            return _dbContext.Notifications.FirstOrDefault(notification => notification.Id == id);
        }

        public void Create(Notification notification)
        {
            _dbContext.Notifications.Add(notification);
            _dbContext.SaveChanges();
        }

        public void Update(Notification notification)
        {
            _dbContext.Entry(notification).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var notificationToDelete = _dbContext.Notifications.Find(id);
            if (notificationToDelete != null)
            {
                _dbContext.Notifications.Remove(notificationToDelete);
                _dbContext.SaveChanges();
            }
        }
    }
}
