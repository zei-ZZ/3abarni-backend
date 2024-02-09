namespace _3abarni_backend.DTOs
{
    public class ContactDto
    {
        public string id { get; set; }
        public string userName { get; set; }
        public string profilePicPath { get; set; }
        public string lastMessageSent { get; set; }
        public DateTime lastMessageSentAt { get; set; }
    }
}
