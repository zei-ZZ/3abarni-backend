using System.Collections.Generic;
using _3abarni_backend.DTOs;
using _3abarni_backend.Mappers;
using _3abarni_backend.Models;
using _3abarni_backend.Repositories;

namespace _3abarni_backend.Services
{
    public class ReactionService
    {
        private readonly ReactionRepository _reactionRepository;

        public ReactionService(ReactionRepository reactionRepository)
        {
            _reactionRepository = reactionRepository;
        }

        public IEnumerable<ReactionDto> GetAll(int chatId, int messageId)
        {
            var reactions = _reactionRepository.GetAll(chatId, messageId);
            return reactions.Select(ReactionMapper.MapToDto);
        }

        public ReactionDto GetById(int id)
        {
            var reaction = _reactionRepository.GetById(id);
            return reaction != null ? ReactionMapper.MapToDto(reaction) : null;
        }

        public void Create(int chatId, int messageId, ReactionDto reactionDto)
        {
            var reaction = new Reaction
            {
                Type = reactionDto.Type,
                MessageId = messageId
            };
            _reactionRepository.Create(reaction);
        }

        public void Update(int id, ReactionDto reactionDto)
        {
            var existingReaction = _reactionRepository.GetById(id);
            if (existingReaction != null)
            {
                existingReaction.Type = reactionDto.Type;
                _reactionRepository.Update(existingReaction);
            }
        }

        public void Delete(int id)
        {
            _reactionRepository.Delete(id);
        }
    }
}
