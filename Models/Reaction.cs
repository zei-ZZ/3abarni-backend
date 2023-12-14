namespace _3abarni_backend.Models
{
    public class Reaction
    {
        public Guid ReactionId { get; set; }
        public string Emoji { get; set; }
        public DateTime Timestamp { get; set; }

        // Foreign Keys
        public string UserId { get; set; }
        public User User { get; set; }

        public Guid MessageId { get; set; }
        public Message Message { get; set; }
    }


}
