namespace ChatMe.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUsersMessagesTie : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Messages", "UserId");
            AddForeignKey("dbo.Messages", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Messages", new[] { "UserId" });
            DropColumn("dbo.Messages", "UserId");
        }
    }
}
