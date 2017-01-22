namespace WebApplicationLogin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_user_connection_in_Quiz_table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quizs", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Quizs", "User_Id");
            AddForeignKey("dbo.Quizs", "User_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Quizs", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Quizs", new[] { "User_Id" });
            DropColumn("dbo.Quizs", "User_Id");
        }
    }
}
