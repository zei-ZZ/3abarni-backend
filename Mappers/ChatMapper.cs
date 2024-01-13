using _3abarni_backend.DTOs;
using _3abarni_backend.Models;

namespace _3abarni_backend.Mappers
{
    public static class ChatMapper
    {
        public static ChatDto MapToDto(Chat chat)
        {
            return new ChatDto
            {
                Id = chat.Id,
                Name = chat.Name
            };
        }
    }
}
