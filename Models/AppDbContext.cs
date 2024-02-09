using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace _3abarni_backend.Models
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Reaction> Reactions { get; set; }


        // We may not need to add DbSet<User> for the User entity since it is managed by IdentityDbContext<User>

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships between entities
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Chat)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ChatId)
                .OnDelete(DeleteBehavior.Cascade); // Adjust the delete behavior as needed

            modelBuilder.Entity<Reaction>()
                .HasOne(r => r.Message)
                .WithMany(m => m.Reactions)
                .HasForeignKey(r => r.MessageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Chat>()
            .HasMany(e => e.Users)
            .WithMany(e => e.Chats) ;
            modelBuilder.Entity<Chat>()
                .Navigation(e => e.Users)
                .AutoInclude();
            modelBuilder.Entity<Chat>()
                .Navigation(e => e.Messages)
                .AutoInclude();

        }
    }


}
