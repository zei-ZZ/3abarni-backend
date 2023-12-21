using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using _3abarni_backend.Models;

namespace _3abarni_backend.Repositories
{
    public class MessageRepository
    {
        private readonly AppDbContext _dbContext;

        public MessageRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Message> GetAll()
        {
            return _dbContext.Messages.ToList();
        }

        public Message GetById(int id)
        {
            return _dbContext.Messages.FirstOrDefault(message => message.Id == id);
        }

        public void Create(Message message)
        {
            _dbContext.Messages.Add(message);
            _dbContext.SaveChanges();
        }

        public void Update(Message message)
        {
            _dbContext.Entry(message).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var messageToDelete = _dbContext.Messages.Find(id);
            if (messageToDelete != null)
            {
                _dbContext.Messages.Remove(messageToDelete);
                _dbContext.SaveChanges();
            }
        }
    }
}
