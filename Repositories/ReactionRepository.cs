using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using _3abarni_backend.Models;

namespace _3abarni_backend.Repositories
{
    public class ReactionRepository
    {
        private readonly AppDbContext _dbContext;

        public ReactionRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Reaction> GetAll(int chatId, int messageId)
        {
            return _dbContext.Reactions
                .Where(reaction => reaction.MessageId == messageId && reaction.Message.ChatId == chatId)
                .ToList();
        }

        public Reaction GetById(int id)
        {
            return _dbContext.Reactions.FirstOrDefault(reaction => reaction.Id == id);
        }

        public void Create(Reaction reaction)
        {
            _dbContext.Reactions.Add(reaction);
            _dbContext.SaveChanges();
        }

        public void Update(Reaction reaction)
        {
            _dbContext.Entry(reaction).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var reactionToDelete = _dbContext.Reactions.Find(id);
            if (reactionToDelete != null)
            {
                _dbContext.Reactions.Remove(reactionToDelete);
                _dbContext.SaveChanges();
            }
        }
    }
}
