namespace SAS_LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEndCourseinCourseModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "EndCourse", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "EndCourse");
        }
    }
}
