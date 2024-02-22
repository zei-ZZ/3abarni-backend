using System.Collections.Generic;
using _3abarni_backend.DTOs;
using _3abarni_backend.Mappers;
using _3abarni_backend.Models;
using _3abarni_backend.Repositories;
using System.Linq;

namespace _3abarni_backend.Services
{
    public class MessageService
    {
        private readonly MessageRepository _messageRepository;

        public MessageService(MessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public IEnumerable<MessageDto> GetAll()
        {
            var messages = _messageRepository.GetAll();
            return messages.Select(MessageMapper.MapToDto);
        }

        public MessageDto GetById(int id)
        {
            var message = _messageRepository.GetById(id);
            return message != null ? MessageMapper.MapToDto(message) : null;
        }


        public void Create(MessageDto messageDto)
        {
            var message = new Message
            {
                Content = messageDto.Content,
                Timestamp = messageDto.Timestamp,
                ChatId = messageDto.ChatId,
                UserId = messageDto.UserId
            };
            _messageRepository.Create(message);
        }

        public void Update(int id, MessageDto messageDto)
        {
            var existingMessage = _messageRepository.GetById(id);
            if (existingMessage != null)
            {
                existingMessage.Content = messageDto.Content;
                existingMessage.Timestamp = messageDto.Timestamp;
                existingMessage.ChatId = messageDto.ChatId;
                _messageRepository.Update(existingMessage);
            }
        }

        public void Delete(int id)
        {
            _messageRepository.Delete(id);
        }
    }
}
