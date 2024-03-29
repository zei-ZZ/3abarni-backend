﻿using Microsoft.AspNetCore.Mvc;
using _3abarni_backend.DTOs;
using _3abarni_backend.Services;
using _3abarni_backend.Mappers;

namespace _3abarni_backend.Controllers
{
    [Route("chats")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ChatService _chatService;

        public ChatController(ChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet("/sender/{senderUsername}/receiver/{receiverUsername}")]
        public IActionResult GetChatByUsers(string senderUsername, string receiverUsername)
        {
            var chats = _chatService.GetChatByUsers(senderUsername, receiverUsername);
            return Ok(chats);
        }

        [HttpGet("messages/sender/{senderUsername}/receiver/{receiverUsername}")]
        public IActionResult GetChatHistory(string senderUsername, string receiverUsername)
        {
            var chats = _chatService.GetChatHistory(senderUsername, receiverUsername);
            return Ok(chats);
        }

        [HttpGet]
        public IActionResult GetAllChats()
        {
            var chats = _chatService.GetAll();
            return Ok(chats);
        }

        [HttpGet("{id}")]
        public IActionResult GetChatById(int id)
        {
            var chat = _chatService.GetById(id);
            if (chat == null)
            {
                return NotFound();
            }
            return Ok(chat);
        }

        [HttpPost]
        public IActionResult CreateChat([FromBody] ChatDto chatDto)
        {
            _chatService.Create(chatDto);
            return CreatedAtAction(nameof(GetChatById), new { id = chatDto.Id }, chatDto);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateChat(int id, [FromBody] ChatDto chatDto)
        {
            _chatService.Update(id, chatDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteChat(int id)
        {
            _chatService.Delete(id);
            return NoContent();
        }
        [HttpGet("/contacts/{id}/{page}")]
        public IActionResult GetContactsByUserPaginated(string id, int page)
        {
            var contacts =_chatService.GetContactsByUserPaginated(id,page);
            return Ok(contacts);
        }
    }
}
