namespace ChatMe.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRequiredFromFirstName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserInfoes", "FirstName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserInfoes", "FirstName", c => c.String(nullable: false));
        }
    }
}
