namespace _3abarni_backend.Models
{
    public class GroupChat : Chat
    {
        public Guid GroupChatId { get; set; }

        // Navigation Property
        public ICollection<UserGroupChat> UserGroupChats { get; set; } = new List<UserGroupChat>();
    }

}
