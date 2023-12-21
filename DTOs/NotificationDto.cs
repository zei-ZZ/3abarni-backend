namespace _3abarni_backend.DTOs
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public string UserId { get; set; } // Include the foreign key for User
    }

}
