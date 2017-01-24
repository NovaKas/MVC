namespace WebApplicationLogin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Remove_user_conn_from_question : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Questions", "userID", "dbo.AspNetUsers");
            DropIndex("dbo.Questions", new[] { "userID" });
            AddColumn("dbo.Questions", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Questions", "userID", c => c.String());
            CreateIndex("dbo.Questions", "ApplicationUser_Id");
            AddForeignKey("dbo.Questions", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Questions", new[] { "ApplicationUser_Id" });
            AlterColumn("dbo.Questions", "userID", c => c.String(maxLength: 128));
            DropColumn("dbo.Questions", "ApplicationUser_Id");
            CreateIndex("dbo.Questions", "userID");
            AddForeignKey("dbo.Questions", "userID", "dbo.AspNetUsers", "Id");
        }
    }
}
