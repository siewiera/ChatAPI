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
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<Session> Sessions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Token>()
                .Property(t => t.Type)
                .HasConversion<string>();

            modelBuilder.Entity<User>(us => 
            {
                us.Property(u => u.CreatedAt)
                .HasColumnType("datetime2(0)");

                us.Property(u => u.ModifiedAt)
                .HasColumnType("datetime2(0)");
            });

            modelBuilder.Entity<Conversation>()
                .Property(c => c.CreatAt)
                .HasColumnType("datetime2(0)");

            modelBuilder.Entity<Message>()
                .Property(m => m.CreationDate)
                .HasColumnType("datetime2(0)");

            modelBuilder.Entity<Token>()
                .Property(t => t.ExpiryDate)
                .HasColumnType("datetime2(0)");


            /*#####################################################*/

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Conversation)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ConversationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Session>()
                .HasOne(s => s.User)
                .WithOne(u => u.Session)
                .HasForeignKey<Session>(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);

        }

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
