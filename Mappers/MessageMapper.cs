using _3abarni_backend.DTOs;
using _3abarni_backend.Models;

namespace _3abarni_backend.Mappers
{
    public static class MessageMapper
    {
        public static MessageDto MapToDto(Message message)
        {
            return new MessageDto
            {
                Id = message.Id,
                Content = message.Content,
                Timestamp = message.Timestamp,
                ChatId = message.ChatId,
                UserId = message.UserId
            };
        }
    }
}
