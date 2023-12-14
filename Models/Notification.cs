namespace _3abarni_backend.Models
{
    public class Notification
    {
        public Guid NotificationId { get; set; }

        // Foreign Key
        public string UserId { get; set; }
        public User User { get; set; }
    }


}
