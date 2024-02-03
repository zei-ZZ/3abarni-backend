using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace _3abarni_backend.Models
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }

        // Foreign key and navigation property to Chat
        public int ChatId { get; set;}
        public Chat Chat { get; set;}
        public User User { get; set; }
        public string UserId { get; set; }

        // Navigation property to reactions
        public ICollection<Reaction> Reactions { get; set; }
    }


}
