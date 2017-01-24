namespace WebApplicationLogin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_quizs_conn_to_question : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Questions", "QuizID", "dbo.Quizs");
            DropIndex("dbo.Questions", new[] { "QuizID" });
            CreateTable(
                "dbo.QuizQuestions",
                c => new
                    {
                        Quiz_QuizID = c.Int(nullable: false),
                        Question_QuestionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Quiz_QuizID, t.Question_QuestionID })
                .ForeignKey("dbo.Quizs", t => t.Quiz_QuizID, cascadeDelete: true)
                .ForeignKey("dbo.Questions", t => t.Question_QuestionID, cascadeDelete: true)
                .Index(t => t.Quiz_QuizID)
                .Index(t => t.Question_QuestionID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuizQuestions", "Question_QuestionID", "dbo.Questions");
            DropForeignKey("dbo.QuizQuestions", "Quiz_QuizID", "dbo.Quizs");
            DropIndex("dbo.QuizQuestions", new[] { "Question_QuestionID" });
            DropIndex("dbo.QuizQuestions", new[] { "Quiz_QuizID" });
            DropTable("dbo.QuizQuestions");
            CreateIndex("dbo.Questions", "QuizID");
            AddForeignKey("dbo.Questions", "QuizID", "dbo.Quizs", "QuizID", cascadeDelete: true);
        }
    }
}
