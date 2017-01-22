namespace WebApplicationLogin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
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
                        Description = c.String(),
                        Weight = c.Int(nullable: false),
                        DateGrade = c.DateTime(nullable: false),
                        MySubjectID = c.Int(nullable: false),
                        userID = c.String(maxLength: 128),
                        GradeListID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GradeID)
                .ForeignKey("dbo.GradeLists", t => t.GradeListID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.userID)
                .Index(t => t.userID)
                .Index(t => t.GradeListID);
            
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
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
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
                "dbo.MySubjects",
                c => new
                    {
                        MySubjectID = c.Int(nullable: false, identity: true),
                        userID = c.String(maxLength: 128),
                        SubjectID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MySubjectID)
                .ForeignKey("dbo.Subjects", t => t.SubjectID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.userID)
                .Index(t => t.userID)
                .Index(t => t.SubjectID);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        SubjectID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        MySubjectID = c.Int(nullable: false),
                        SClassID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SubjectID);
            
            CreateTable(
                "dbo.SClasses",
                c => new
                    {
                        SClassID = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        userID = c.String(),
                    })
                .PrimaryKey(t => t.SClassID)
                .ForeignKey("dbo.AspNetUsers", t => t.SClassID)
                .Index(t => t.SClassID);
            
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
                "dbo.Questions",
                c => new
                    {
                        QuestionID = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        GoodAnswer = c.String(),
                        BadAnswer = c.String(),
                        Points = c.Int(nullable: false),
                        userID = c.String(maxLength: 128),
                        QuizID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QuestionID)
                .ForeignKey("dbo.Quizs", t => t.QuizID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.userID)
                .Index(t => t.userID)
                .Index(t => t.QuizID);
            
            CreateTable(
                "dbo.Quizs",
                c => new
                    {
                        QuizID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Timer = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QuizID);
            
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
                "dbo.SClassSubjects",
                c => new
                    {
                        SClass_SClassID = c.String(nullable: false, maxLength: 128),
                        Subject_SubjectID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SClass_SClassID, t.Subject_SubjectID })
                .ForeignKey("dbo.SClasses", t => t.SClass_SClassID, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.Subject_SubjectID, cascadeDelete: true)
                .Index(t => t.SClass_SClassID)
                .Index(t => t.Subject_SubjectID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Questions", "userID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Questions", "QuizID", "dbo.Quizs");
            DropForeignKey("dbo.News", "userID", "dbo.AspNetUsers");
            DropForeignKey("dbo.MySubjects", "userID", "dbo.AspNetUsers");
            DropForeignKey("dbo.MySubjects", "SubjectID", "dbo.Subjects");
            DropForeignKey("dbo.SClasses", "SClassID", "dbo.AspNetUsers");
            DropForeignKey("dbo.SClassSubjects", "Subject_SubjectID", "dbo.Subjects");
            DropForeignKey("dbo.SClassSubjects", "SClass_SClassID", "dbo.SClasses");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Grades", "userID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Grades", "GradeListID", "dbo.GradeLists");
            DropIndex("dbo.SClassSubjects", new[] { "Subject_SubjectID" });
            DropIndex("dbo.SClassSubjects", new[] { "SClass_SClassID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Questions", new[] { "QuizID" });
            DropIndex("dbo.Questions", new[] { "userID" });
            DropIndex("dbo.News", new[] { "userID" });
            DropIndex("dbo.SClasses", new[] { "SClassID" });
            DropIndex("dbo.MySubjects", new[] { "SubjectID" });
            DropIndex("dbo.MySubjects", new[] { "userID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Grades", new[] { "GradeListID" });
            DropIndex("dbo.Grades", new[] { "userID" });
            DropTable("dbo.SClassSubjects");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Quizs");
            DropTable("dbo.Questions");
            DropTable("dbo.News");
            DropTable("dbo.SClasses");
            DropTable("dbo.Subjects");
            DropTable("dbo.MySubjects");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Grades");
            DropTable("dbo.GradeLists");
        }
    }
}
