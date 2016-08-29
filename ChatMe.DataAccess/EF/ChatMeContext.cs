using ChatMe.DataAccess.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;

namespace ChatMe.DataAccess.EF
{
    public class ChatMeContext : IdentityDbContext<User>
    {
        public ChatMeContext() : base("ChatMe") { }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Dialog> Dialogs { get; set; }
        public DbSet<UserInfo> UserInfoes { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Dialogs)
                .WithMany(d => d.Users);

            modelBuilder.Entity<Message>()
                .HasRequired(m => m.Dialog)
                .WithMany(d => d.Messages)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Message>()
                .HasRequired(m => m.User)
                .WithMany(u => u.Messages);             

            //modelBuilder.Entity<User>()
            //    .HasMany(u => u.Posts)
            //    .WithRequired()
        }

        public static ChatMeContext Create()
        {
            return new ChatMeContext();
        }

        protected override void Dispose(bool disposing) { }
    }
}
