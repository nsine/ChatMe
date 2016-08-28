namespace ChatMe.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTimestampToDialog : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Dialogs", "LastMessageTime", c => c.DateTime());
            AddColumn("dbo.Dialogs", "CreateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Dialogs", "CreateTime");
            DropColumn("dbo.Dialogs", "LastMessageTime");
        }
    }
}
