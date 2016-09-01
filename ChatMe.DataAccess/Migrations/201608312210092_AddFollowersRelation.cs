namespace ChatMe.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFollowersRelation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FollowerLinks",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        FollowerId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.FollowerId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.FollowerId)
                .Index(t => t.UserId)
                .Index(t => t.FollowerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FollowerLinks", "FollowerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FollowerLinks", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.FollowerLinks", new[] { "FollowerId" });
            DropIndex("dbo.FollowerLinks", new[] { "UserId" });
            DropTable("dbo.FollowerLinks");
        }
    }
}
