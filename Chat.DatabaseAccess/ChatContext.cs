using ChatSystem.Model.Chats;
using ChatSystem.Model.Images;
using ChatSystem.Model.Messages;
using ChatSystem.Model.Users;
using Microsoft.EntityFrameworkCore;

namespace ChatSystem.DatabaseAccess
{
    public class ChatContext : DbContext
    {
        public ChatContext(DbContextOptions<ChatContext> options) : base(options)
        {
        }
        public DbSet<Image> Images { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatsUsers> ChatsUsers { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
