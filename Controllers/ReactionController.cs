using Microsoft.AspNetCore.Mvc;
using _3abarni_backend.DTOs;
using _3abarni_backend.Services;

namespace _3abarni_backend.Controllers
{
    [Route("chats/{chatId}/messages/{messageId}/reactions")]
    [ApiController]
    public class ReactionController : ControllerBase
    {
        private readonly ReactionService _reactionService;

        public ReactionController(ReactionService reactionService)
        {
            _reactionService = reactionService;
        }

        [HttpGet]
        public IActionResult GetAllReactions(int chatId, int messageId)
        {
            var reactions = _reactionService.GetAll(chatId, messageId);
            return Ok(reactions);
        }

        [HttpGet("{id}")]
        public IActionResult GetReactionById(int chatId, int messageId, int id)
        {
            var reaction = _reactionService.GetById(id);
            if (reaction == null)
            {
                return NotFound();
            }
            return Ok(reaction);
        }

        [HttpPost]
        public IActionResult CreateReaction(int chatId, int messageId, [FromBody] ReactionDto reactionDto)
        {
            _reactionService.Create(chatId, messageId, reactionDto);
            return CreatedAtAction(nameof(GetReactionById), new { chatId, messageId, id = reactionDto.Id }, reactionDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateReaction(int id, [FromBody] ReactionDto reactionDto)
        {
            _reactionService.Update(id, reactionDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReaction(int id)
        {
            _reactionService.Delete(id);
            return NoContent();
        }
    }
}
