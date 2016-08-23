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

            modelBuilder.Entity<Dialog>()
                .HasRequired(d => d.FirstUser)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dialog>()
                .HasRequired(m => m.SecondUser)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dialog>()
                .HasMany(d => d.Messages)
                .WithRequired()
                .WillCascadeOnDelete(true);
        }

        public static ChatMeContext Create()
        {
            return new ChatMeContext();
        }
    }
}
