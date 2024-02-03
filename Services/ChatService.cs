using System.Collections.Generic;
using _3abarni_backend.DTOs;
using _3abarni_backend.Mappers;
using _3abarni_backend.Models;
using _3abarni_backend.Repositories;

namespace _3abarni_backend.Services
{
    public class ChatService
    {
        private readonly ChatRepository _chatRepository;
        private readonly UserRepository _userRepository;
        public ChatService(ChatRepository chatRepository, UserRepository userRepository)
        {
            _chatRepository = chatRepository;
            _userRepository = userRepository;

        }

        public IEnumerable<ChatDto> GetAll()
        {
            var chats = _chatRepository.GetAll();
            return chats.Select(ChatMapper.MapToDto);
        }

        public ChatDto GetById(int id)
        {
            var chat = _chatRepository.GetById(id);
            return chat != null ? ChatMapper.MapToDto(chat) : null;
        }

        public ChatDto GetOrCreateChat(List<string> UserIds)
        {
            var existingChat = _chatRepository.GetChatByUsers(UserIds);

            if (existingChat != null)
            {
                return ChatMapper.MapToDto(existingChat);
            }

            var newChat = new Chat
            {
                Name = GenerateChatName(UserIds),
                Users = new List<User>()
            };

            foreach (var userId in UserIds)
            {
                User user = _userRepository.GetById(userId);
                if (user != null)
                {
                    newChat.Users.Add(user);
                }
                else
                {
                    // Handle the case where a user with the given ID is not found
                    // You might want to log this or handle it according to your requirements
                }
            }

            _chatRepository.Create(newChat);

            // Use ChatMapper to convert the entire newChat object to a ChatDto
            var chatDto = ChatMapper.MapToDto(newChat);
            chatDto.UserIds = newChat.Users.Select(user => user.Id).ToList(); // Set the UserIds property

            return chatDto;
        }

        private string GenerateChatName(List<string> UserIds)
        {
            // Implement your logic to generate a meaningful chat name based on the user IDs
            // For example, concatenate user names or use a default name
            return "Chat_" + string.Join("_", UserIds);
        }

        public void Create(ChatDto chatDto)
        {
            var chat = new Chat { Name = chatDto.Name };
            _chatRepository.Create(chat);
        }

        public void Update(int id, ChatDto chatDto)
        {
            var existingChat = _chatRepository.GetById(id);
            if (existingChat != null)
            {
                existingChat.Name = chatDto.Name;
                _chatRepository.Update(existingChat);
            }
        }

        public void Delete(int id)
        {
            _chatRepository.Delete(id);
        }
    }
}
