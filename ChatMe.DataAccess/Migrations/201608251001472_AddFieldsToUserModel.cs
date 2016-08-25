namespace ChatMe.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldsToUserModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserInfoes", "Skype", c => c.String());
            AddColumn("dbo.UserInfoes", "AvatarFilename", c => c.String());
            AddColumn("dbo.UserInfoes", "AvatarMimeType", c => c.String());
            AlterColumn("dbo.UserInfoes", "Phone", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserInfoes", "Phone", c => c.Int(nullable: false));
            DropColumn("dbo.UserInfoes", "AvatarMimeType");
            DropColumn("dbo.UserInfoes", "AvatarFilename");
            DropColumn("dbo.UserInfoes", "Skype");
        }
    }
}
