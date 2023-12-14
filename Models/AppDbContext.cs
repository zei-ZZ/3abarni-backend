using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace _3abarni_backend.Models
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public DbSet<Chat> Chats { get; set; }
        public DbSet<PairChat> PairChats { get; set; }
        public DbSet<GroupChat> GroupChats { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Reaction> Reactions { get; set; }

        // Include the UserGroupChat intermediate table for many-to-many relationship
        public DbSet<UserGroupChat> UserGroupChats { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships using Fluent API
            modelBuilder.Entity<Chat>()
                .HasMany(c => c.Notifications)
                .WithOne(n => n.Chat)
                .HasForeignKey(n => n.ChatId);

            modelBuilder.Entity<Chat>()
                .HasMany(c => c.Messages)
                .WithOne(m => m.Chat)
                .HasForeignKey(m => m.ChatId);

            modelBuilder.Entity<PairChat>()
                .HasMany(pc => pc.Users)
                .WithMany(u => u.PairChats)
                .UsingEntity<UserPairChat>(
                    j => j.HasOne(upc => upc.User)
                          .WithMany(u => u.UserPairChats)
                          .HasForeignKey(upc => upc.UserId),
                    j => j.HasOne(upc => upc.PairChat)
                          .WithMany(pc => pc.PairChatUsers)
                          .HasForeignKey(upc => upc.PairChatId),
                    j =>
                    {
                        j.HasKey(upc => new { upc.UserId, upc.PairChatId });
                        j.ToTable("UserPairChats");
                    });

            modelBuilder.Entity<GroupChat>()
                .HasMany(gc => gc.UserGroupChats)
                .WithOne(ugc => ugc.GroupChat)
                .HasForeignKey(ugc => ugc.GroupChatId);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Chat)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ChatId);

            modelBuilder.Entity<Reaction>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reactions)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Reaction>()
                .HasOne(r => r.Message)
                .WithMany(m => m.Reactions)
                .HasForeignKey(r => r.MessageId);
        }
    }

    // Intermediate table for many-to-many relationship between User and PairChat
    public class UserPairChat
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public Guid PairChatId { get; set; }
        public PairChat PairChat { get; set; }
    }

    // Intermediate table for many-to-many relationship between User and GroupChat
    public class UserGroupChat
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public Guid GroupChatId { get; set; }
        public GroupChat GroupChat { get; set; }
    }
}
