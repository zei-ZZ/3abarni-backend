using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _3abarni_backend.Models
{
    public class User : IdentityUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override string Id { get; set; }
        public string ProfilePicPath { get; set; } = string.Empty;
        [NotMapped]
        public IFormFile ProfilePic { get; set; }

        // Navigation Properties
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        public ICollection<Message> Messages { get; set; } = new List<Message>();
        public ICollection<Reaction> Reactions { get; set; } = new List<Reaction>();

        // Navigation Property for Chat (assuming many-to-many relationship)
        public ICollection<Chat> Chats { get; set; } = new List<Chat>();
    }


}
