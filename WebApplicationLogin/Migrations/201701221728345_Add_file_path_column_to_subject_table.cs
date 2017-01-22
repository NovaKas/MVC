namespace WebApplicationLogin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_file_path_column_to_subject_table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subjects", "FilePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subjects", "FilePath");
        }
    }
}
