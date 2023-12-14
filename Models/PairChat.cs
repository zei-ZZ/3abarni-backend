namespace _3abarni_backend.Models
{
    public class PairChat : Chat
    {
        public Guid PairChatId { get; set; }

        // Navigation Property
        public ICollection<User> Users { get; set; } = new List<User>();
    }

}
