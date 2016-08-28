namespace ChatMe.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContextChange : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.DialogUsers", newName: "UserDialogs");
            DropForeignKey("dbo.Dialogs", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Dialogs", new[] { "User_Id" });
            DropPrimaryKey("dbo.UserDialogs");
            AddPrimaryKey("dbo.UserDialogs", new[] { "User_Id", "Dialog_Id" });
            DropColumn("dbo.Dialogs", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Dialogs", "User_Id", c => c.String(maxLength: 128));
            DropPrimaryKey("dbo.UserDialogs");
            AddPrimaryKey("dbo.UserDialogs", new[] { "Dialog_Id", "User_Id" });
            CreateIndex("dbo.Dialogs", "User_Id");
            AddForeignKey("dbo.Dialogs", "User_Id", "dbo.AspNetUsers", "Id");
            RenameTable(name: "dbo.UserDialogs", newName: "DialogUsers");
        }
    }
}
