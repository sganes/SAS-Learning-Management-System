namespace SAS_LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCourseIdinIdentityModel : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.AspNetUsers", name: "Course_Id", newName: "CourseId");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_Course_Id", newName: "IX_CourseId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_CourseId", newName: "IX_Course_Id");
            RenameColumn(table: "dbo.AspNetUsers", name: "CourseId", newName: "Course_Id");
        }
    }
}
