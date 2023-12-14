using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace _3abarni_backend.Models
{
    public class User : IdentityUser
    {
        public string ProfilePicPath { get; set; } = string.Empty;
        [NotMapped]
        public IFormFile ProfilePic { get; set; }

        // Navigation Properties
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        public ICollection<Message> Messages { get; set; } = new List<Message>();
        public ICollection<Reaction> Reactions { get; set; } = new List<Reaction>();

        // Navigation Property for GroupChat (assuming many-to-many relationship)
        public ICollection<UserGroupChat> UserGroupChats { get; set; } = new List<UserGroupChat>();

        // Navigation Property for PairChat (assuming many-to-many relationship)
        public ICollection<PairChat> PairChats { get; set; } = new List<PairChat>();
    }


}
