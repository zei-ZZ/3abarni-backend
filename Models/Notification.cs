using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace _3abarni_backend.Models
{
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }

        // Foreign key and navigation property to User (assuming a relationship with users)
        public string UserId { get; set; }
        public User User { get; set; }
    }


}
