namespace WebApplicationLogin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class third_chance : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GradeLists",
                c => new
                    {
                        GradeListID = c.Int(nullable: false, identity: true),
                        Value = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.GradeListID);
            
            CreateTable(
                "dbo.Grades",
                c => new
                    {
                        GradeID = c.Int(nullable: false, identity: true),
                        StudentID = c.Int(nullable: false),
                        GradeListID = c.Int(nullable: false),
                        Weight = c.Single(nullable: false),
                        Description = c.String(),
                        DateGrade = c.DateTime(nullable: false),
                        SubjectID = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.GradeID)
                .ForeignKey("dbo.GradeLists", t => t.GradeListID, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentID, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.SubjectID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.StudentID)
                .Index(t => t.GradeListID)
                .Index(t => t.SubjectID)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SClassID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StudentID)
                .ForeignKey("dbo.SClasses", t => t.SClassID, cascadeDelete: true)
                .Index(t => t.SClassID);
            
            CreateTable(
                "dbo.SClasses",
                c => new
                    {
                        SClassID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TeacherID = c.Int(nullable: false),
                        PlanID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SClassID)
                .ForeignKey("dbo.Teachers", t => t.TeacherID, cascadeDelete: true)
                .ForeignKey("dbo.Plans", t => t.PlanID, cascadeDelete: true)
                .Index(t => t.TeacherID)
                .Index(t => t.PlanID);
            
            CreateTable(
                "dbo.Plans",
                c => new
                    {
                        PlanID = c.Int(nullable: false, identity: true),
                        Nazwa = c.String(),
                        SubjectID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PlanID);
            
            CreateTable(
                "dbo.PlanSubjects",
                c => new
                    {
                        PlanSubjectID = c.Int(nullable: false, identity: true),
                        PlanID = c.Int(nullable: false),
                        SubjectID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PlanSubjectID)
                .ForeignKey("dbo.Plans", t => t.PlanID, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.SubjectID, cascadeDelete: true)
                .Index(t => t.PlanID)
                .Index(t => t.SubjectID);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        SubjectID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        FilePath = c.String(),
                    })
                .PrimaryKey(t => t.SubjectID);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        TeacherID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Subject_SubjectID = c.Int(),
                    })
                .PrimaryKey(t => t.TeacherID)
                .ForeignKey("dbo.Subjects", t => t.Subject_SubjectID)
                .Index(t => t.Subject_SubjectID);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        NewsID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        userID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.NewsID)
                .ForeignKey("dbo.AspNetUsers", t => t.userID)
                .Index(t => t.userID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Surname = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        SClass_SClassID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SClasses", t => t.SClass_SClassID)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.SClass_SClassID);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        QuestionID = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        GoodAnswer = c.String(),
                        BadAnswer = c.String(),
                        Points = c.Int(nullable: false),
                        userID = c.String(),
                        QuizID = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.QuestionID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Quizs",
                c => new
                    {
                        QuizID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Timer = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.QuizID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
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
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUsers", "SClass_SClassID", "dbo.SClasses");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Questions", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Quizs", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.QuizQuestions", "Question_QuestionID", "dbo.Questions");
            DropForeignKey("dbo.QuizQuestions", "Quiz_QuizID", "dbo.Quizs");
            DropForeignKey("dbo.News", "userID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Grades", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Grades", "SubjectID", "dbo.Subjects");
            DropForeignKey("dbo.Students", "SClassID", "dbo.SClasses");
            DropForeignKey("dbo.SClasses", "PlanID", "dbo.Plans");
            DropForeignKey("dbo.Teachers", "Subject_SubjectID", "dbo.Subjects");
            DropForeignKey("dbo.SClasses", "TeacherID", "dbo.Teachers");
            DropForeignKey("dbo.PlanSubjects", "SubjectID", "dbo.Subjects");
            DropForeignKey("dbo.PlanSubjects", "PlanID", "dbo.Plans");
            DropForeignKey("dbo.Grades", "StudentID", "dbo.Students");
            DropForeignKey("dbo.Grades", "GradeListID", "dbo.GradeLists");
            DropIndex("dbo.QuizQuestions", new[] { "Question_QuestionID" });
            DropIndex("dbo.QuizQuestions", new[] { "Quiz_QuizID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Quizs", new[] { "User_Id" });
            DropIndex("dbo.Questions", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "SClass_SClassID" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.News", new[] { "userID" });
            DropIndex("dbo.Teachers", new[] { "Subject_SubjectID" });
            DropIndex("dbo.PlanSubjects", new[] { "SubjectID" });
            DropIndex("dbo.PlanSubjects", new[] { "PlanID" });
            DropIndex("dbo.SClasses", new[] { "PlanID" });
            DropIndex("dbo.SClasses", new[] { "TeacherID" });
            DropIndex("dbo.Students", new[] { "SClassID" });
            DropIndex("dbo.Grades", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Grades", new[] { "SubjectID" });
            DropIndex("dbo.Grades", new[] { "GradeListID" });
            DropIndex("dbo.Grades", new[] { "StudentID" });
            DropTable("dbo.QuizQuestions");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Quizs");
            DropTable("dbo.Questions");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.News");
            DropTable("dbo.Teachers");
            DropTable("dbo.Subjects");
            DropTable("dbo.PlanSubjects");
            DropTable("dbo.Plans");
            DropTable("dbo.SClasses");
            DropTable("dbo.Students");
            DropTable("dbo.Grades");
            DropTable("dbo.GradeLists");
        }
    }
}
