namespace _3abarni_backend.DTOs
{
    public class ChatDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<string> UserIds { get; set; } = new List<string>();
    }
}
