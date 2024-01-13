using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace _3abarni_backend.Models
{
    public class Reaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Type { get; set; } // e.g., "like", "dislike"

        // Foreign key and navigation property to Message
        public int MessageId { get; set; }
        public Message Message { get; set; }
    }


}
