using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace _3abarni_backend.Models
{
        public class Chat
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public string Name { get; set; }


            // Navigation property
            public ICollection<Message> Messages { get; set; }

            public ICollection<User> Users { get; set; }
    }

}
