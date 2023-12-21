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

        public ChatService(ChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
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
