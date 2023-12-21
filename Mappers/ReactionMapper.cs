using _3abarni_backend.DTOs;
using _3abarni_backend.Models;

namespace _3abarni_backend.Mappers
{
    public static class ReactionMapper
    {
        public static ReactionDto MapToDto(Reaction reaction)
        {
            return new ReactionDto
            {
                Id = reaction.Id,
                Type = reaction.Type,
                MessageId = reaction.MessageId
            };
        }
    }
}
