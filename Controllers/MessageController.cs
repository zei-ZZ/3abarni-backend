using Microsoft.AspNetCore.Mvc;
using _3abarni_backend.DTOs;
using _3abarni_backend.Services;

namespace _3abarni_backend.Controllers
{
    [Route("chats/{chatId}/messages")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly MessageService _messageService;

        public MessageController(MessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public IActionResult GetAllMessages(int chatId)
        {
            var messages = _messageService.GetAll();
            return Ok(messages);
        }

        [HttpGet("{id}")]
        public IActionResult GetMessageById(int chatId, int id)
        {
            var message = _messageService.GetById(id);
            if (message == null)
            {
                return NotFound();
            }
            return Ok(message);
        }

        [HttpPost]
        public IActionResult CreateMessage(int chatId, [FromBody] MessageDto messageDto)
        {
            messageDto.ChatId = chatId; // Assign the chatId from the route
            _messageService.Create(messageDto);
            return CreatedAtAction(nameof(GetMessageById), new { chatId, id = messageDto.Id }, messageDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMessage(int chatId, int id, [FromBody] MessageDto messageDto)
        {
            messageDto.ChatId = chatId; // Assign the chatId from the route
            _messageService.Update(id, messageDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMessage(int chatId, int id)
        {
            _messageService.Delete(id);
            return NoContent();
        }
    }
}
