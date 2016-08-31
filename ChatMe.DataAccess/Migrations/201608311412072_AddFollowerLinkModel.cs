namespace ChatMe.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFollowerLinkModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FollowerLinks",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        FollowingUserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.FollowingUserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.FollowingUserId)
                .Index(t => t.UserId)
                .Index(t => t.FollowingUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FollowerLinks", "FollowingUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FollowerLinks", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.FollowerLinks", new[] { "FollowingUserId" });
            DropIndex("dbo.FollowerLinks", new[] { "UserId" });
            DropTable("dbo.FollowerLinks");
        }
    }
}
