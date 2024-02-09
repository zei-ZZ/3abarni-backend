using _3abarni_backend.DTOs;
using _3abarni_backend.Models;

namespace _3abarni_backend.Mappers
{
    public static class ChatMapper
    {
        public static ChatDto MapToDto(Chat chat)
        {
            // Extract user IDs from the Users collection
            var userIds = chat.Users.Select(u => u.Id).ToList();

            return new ChatDto
            {
                Id = chat.Id,
                Name = chat.Name,
                UserIds = userIds
            };
        }
    }
}
