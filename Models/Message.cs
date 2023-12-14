namespace _3abarni_backend.Models
{
    public class Message
    {
        public Guid MessageId { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }

        // Foreign Key
        public Guid ChatId { get; set; }
        public Chat Chat { get; set; }

        // Navigation Property
        public ICollection<Reaction> Reactions { get; set; } = new List<Reaction>();
    }


}
