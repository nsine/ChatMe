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
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.DialogUsers",
                c => new
                    {
                        Dialog_Id = c.Int(nullable: false),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Dialog_Id, t.User_Id })
                .ForeignKey("dbo.Dialogs", t => t.Dialog_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Dialog_Id)
                .Index(t => t.User_Id);
            
            AddColumn("dbo.Messages", "DialogId", c => c.Int(nullable: false));
            CreateIndex("dbo.Messages", "DialogId");
            AddForeignKey("dbo.Messages", "DialogId", "dbo.Dialogs", "Id", cascadeDelete: true);
            DropColumn("dbo.Messages", "UserFromId");
            DropColumn("dbo.Messages", "UserToId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "UserToId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Messages", "UserFromId", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.Messages", "DialogId", "dbo.Dialogs");
            DropForeignKey("dbo.DialogUsers", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.DialogUsers", "Dialog_Id", "dbo.Dialogs");
            DropForeignKey("dbo.Dialogs", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.DialogUsers", new[] { "User_Id" });
            DropIndex("dbo.DialogUsers", new[] { "Dialog_Id" });
            DropIndex("dbo.Dialogs", new[] { "User_Id" });
            DropIndex("dbo.Messages", new[] { "DialogId" });
            DropColumn("dbo.Messages", "DialogId");
            DropTable("dbo.DialogUsers");
            DropTable("dbo.Dialogs");
            CreateIndex("dbo.Messages", "UserToId");
            CreateIndex("dbo.Messages", "UserFromId");
            AddForeignKey("dbo.Messages", "UserToId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Messages", "UserFromId", "dbo.AspNetUsers", "Id");
        }
    }
}
