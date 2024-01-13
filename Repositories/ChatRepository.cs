using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using _3abarni_backend.Models;

namespace _3abarni_backend.Repositories
{
    public class ChatRepository
    {
        private readonly AppDbContext _dbContext;

        public ChatRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Chat> GetAll()
        {
            return _dbContext.Chats.ToList();
        }

        public Chat GetById(int id)
        {
            return _dbContext.Chats.FirstOrDefault(chat => chat.Id == id);
        }

        public void Create(Chat chat)
        {
            _dbContext.Chats.Add(chat);
            _dbContext.SaveChanges();
        }

        public void Update(Chat chat)
        {
            _dbContext.Entry(chat).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var chatToDelete = _dbContext.Chats.Find(id);
            if (chatToDelete != null)
            {
                _dbContext.Chats.Remove(chatToDelete);
                _dbContext.SaveChanges();
            }
        }
    }
}
