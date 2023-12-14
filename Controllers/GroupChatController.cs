using Microsoft.AspNetCore.Mvc;

namespace _3abarni_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupChatController : ControllerBase
    {
        private readonly GroupChatService _groupChatService;

        public GroupChatController(GroupChatService groupChatService)
        {
            _groupChatService = groupChatService;
        }

        [HttpPost("create")]
        public IActionResult CreateGroupChat(List<string> userIds)
        {
            try
            {
                var groupChatId = _groupChatService.CreateGroupChat(userIds);
                return Ok(new { GroupChatId = groupChatId });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Error: {ex.Message}" });
            }
        }

        // Add more endpoints for GroupChat operations
    }

}
