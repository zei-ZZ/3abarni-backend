namespace _3abarni_backend.Models
{
    public class Chat
{
    public Guid ChatId { get; set; }

    // Navigation Properties
    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    public ICollection<Message> Messages { get; set; } = new List<Message>();
}

}
