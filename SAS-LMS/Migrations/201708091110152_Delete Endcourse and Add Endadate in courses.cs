namespace SAS_LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteEndcourseandAddEndadateincourses : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "EndDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Courses", "EndCourse");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "EndCourse", c => c.Boolean(nullable: false));
            DropColumn("dbo.Courses", "EndDate");
        }
    }
}
