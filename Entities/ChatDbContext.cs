using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace ChatAPI.Entities
{
    public class ChatDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public ChatDbContext(DbContextOptions<ChatDbContext> option, IConfiguration configuration) : base(option)
        {
            _configuration = configuration;
        }

        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserConversation> UserConversations { get; set; }
        public DbSet<Channel> Channels { get; set; }


        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }*/

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string[] connectionType = { "PrivateConnection", "BusinessConnection" };
                var connectionString = _configuration.GetConnectionString(connectionType[1]);

                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
