using ChatMe.DataAccess.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace ChatMe.DataAccess.EF
{
    public class ChatMeContext : IdentityDbContext<User>
    {
        public ChatMeContext() : base("ChatMe") { }

        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Message>()
                .HasRequired(m => m.UserFrom)
                .WithMany(u => u.SentMessages)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Message>()
                .HasRequired(m => m.UserTo)
                .WithMany(u => u.ReceivedMessages)
                .WillCascadeOnDelete(false);
        }

        public static ChatMeContext Create()
        {
            return new ChatMeContext();
        }
    }
}
