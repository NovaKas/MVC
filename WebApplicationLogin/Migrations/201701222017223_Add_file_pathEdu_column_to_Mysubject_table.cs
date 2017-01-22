namespace WebApplicationLogin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_file_pathEdu_column_to_Mysubject_table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MySubjects", "FilePathEdu", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MySubjects", "FilePathEdu");
        }
    }
}
