using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using _3abarni_backend.Models;
using _3abarni_backend.DTOs;

namespace _3abarni_backend.Repositories
{
    public class ChatRepository
    {
        const int NUMBER_OF_ITEMS_PER_PAGE = 6;
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

        public Chat GetChatByUsers(IEnumerable<string> UserIds)
        {
            var chat = _dbContext.Chats
                .Where(c => c.Users.All(user => UserIds.Contains(user.Id)))
                .FirstOrDefault();

            return chat;
        }
        public IEnumerable<Chat> getContactsByUserPaginated(string id , int page)
        {
            var chats= _dbContext.Chats
                 .Where(c => c.Users.All(user => user.Id ==id))
                 .OrderBy(c=>c.Messages.)
                 .Skip((page - 1) * NUMBER_OF_ITEMS_PER_PAGE)
                 .Take(NUMBER_OF_ITEMS_PER_PAGE)
                 .ToList();
            return chats;

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
