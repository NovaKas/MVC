namespace WebApplicationLogin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_plan_Modif_SClass_Subject : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SClassSubjects", "SClass_SClassID", "dbo.SClasses");
            DropForeignKey("dbo.SClassSubjects", "Subject_SubjectID", "dbo.Subjects");
            DropIndex("dbo.SClassSubjects", new[] { "SClass_SClassID" });
            DropIndex("dbo.SClassSubjects", new[] { "Subject_SubjectID" });
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
                "dbo.Plans",
                c => new
                    {
                        PlanID = c.Int(nullable: false, identity: true),
                        Nazwa = c.String(),
                        SubjectID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PlanID);
            
            AddColumn("dbo.SClasses", "PlanID", c => c.Int(nullable: false));
            CreateIndex("dbo.SClasses", "PlanID");
            AddForeignKey("dbo.SClasses", "PlanID", "dbo.Plans", "PlanID", cascadeDelete: true);
            DropColumn("dbo.Subjects", "MySubjectID");
            DropColumn("dbo.Subjects", "SClassID");
            DropTable("dbo.SClassSubjects");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SClassSubjects",
                c => new
                    {
                        SClass_SClassID = c.String(nullable: false, maxLength: 128),
                        Subject_SubjectID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SClass_SClassID, t.Subject_SubjectID });
            
            AddColumn("dbo.Subjects", "SClassID", c => c.Int(nullable: false));
            AddColumn("dbo.Subjects", "MySubjectID", c => c.Int(nullable: false));
            DropForeignKey("dbo.PlanSubjects", "SubjectID", "dbo.Subjects");
            DropForeignKey("dbo.SClasses", "PlanID", "dbo.Plans");
            DropForeignKey("dbo.PlanSubjects", "PlanID", "dbo.Plans");
            DropIndex("dbo.SClasses", new[] { "PlanID" });
            DropIndex("dbo.PlanSubjects", new[] { "SubjectID" });
            DropIndex("dbo.PlanSubjects", new[] { "PlanID" });
            DropColumn("dbo.SClasses", "PlanID");
            DropTable("dbo.Plans");
            DropTable("dbo.PlanSubjects");
            CreateIndex("dbo.SClassSubjects", "Subject_SubjectID");
            CreateIndex("dbo.SClassSubjects", "SClass_SClassID");
            AddForeignKey("dbo.SClassSubjects", "Subject_SubjectID", "dbo.Subjects", "SubjectID", cascadeDelete: true);
            AddForeignKey("dbo.SClassSubjects", "SClass_SClassID", "dbo.SClasses", "SClassID", cascadeDelete: true);
        }
    }
}
