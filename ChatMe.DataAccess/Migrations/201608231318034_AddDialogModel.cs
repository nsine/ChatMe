namespace ChatMe.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDialogModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "UserFromId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "UserToId", "dbo.AspNetUsers");
            DropIndex("dbo.Messages", new[] { "UserFromId" });
            DropIndex("dbo.Messages", new[] { "UserToId" });
            CreateTable(
                "dbo.Dialogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstUserId = c.String(nullable: false, maxLength: 128),
                        SecondUserId = c.String(nullable: false, maxLength: 128),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.FirstUserId)
                .ForeignKey("dbo.AspNetUsers", t => t.SecondUserId)
                .Index(t => t.FirstUserId)
                .Index(t => t.SecondUserId)
                .Index(t => t.User_Id);
            
            AddColumn("dbo.Messages", "DialogId", c => c.Int(nullable: false));
            AddColumn("dbo.Messages", "Dialog_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Messages", "DialogId");
            CreateIndex("dbo.Messages", "Dialog_Id");
            AddForeignKey("dbo.Messages", "Dialog_Id", "dbo.Dialogs", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Messages", "DialogId", "dbo.Dialogs", "Id", cascadeDelete: true);
            DropColumn("dbo.Messages", "UserFromId");
            DropColumn("dbo.Messages", "UserToId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "UserToId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Messages", "UserFromId", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.Messages", "DialogId", "dbo.Dialogs");
            DropForeignKey("dbo.Dialogs", "SecondUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "Dialog_Id", "dbo.Dialogs");
            DropForeignKey("dbo.Dialogs", "FirstUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Dialogs", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Dialogs", new[] { "User_Id" });
            DropIndex("dbo.Dialogs", new[] { "SecondUserId" });
            DropIndex("dbo.Dialogs", new[] { "FirstUserId" });
            DropIndex("dbo.Messages", new[] { "Dialog_Id" });
            DropIndex("dbo.Messages", new[] { "DialogId" });
            DropColumn("dbo.Messages", "Dialog_Id");
            DropColumn("dbo.Messages", "DialogId");
            DropTable("dbo.Dialogs");
            CreateIndex("dbo.Messages", "UserToId");
            CreateIndex("dbo.Messages", "UserFromId");
            AddForeignKey("dbo.Messages", "UserToId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Messages", "UserFromId", "dbo.AspNetUsers", "Id");
        }
    }
}
