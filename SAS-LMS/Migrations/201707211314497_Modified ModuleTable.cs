namespace SAS_LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedModuleTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Modules", "Course_Id", "dbo.Courses");
            DropIndex("dbo.Modules", new[] { "Course_Id" });
            RenameColumn(table: "dbo.Modules", name: "Course_Id", newName: "CourseId");
            AlterColumn("dbo.Modules", "CourseId", c => c.Int(nullable: false));
            CreateIndex("dbo.Modules", "CourseId");
            AddForeignKey("dbo.Modules", "CourseId", "dbo.Courses", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Modules", "CourseId", "dbo.Courses");
            DropIndex("dbo.Modules", new[] { "CourseId" });
            AlterColumn("dbo.Modules", "CourseId", c => c.Int());
            RenameColumn(table: "dbo.Modules", name: "CourseId", newName: "Course_Id");
            CreateIndex("dbo.Modules", "Course_Id");
            AddForeignKey("dbo.Modules", "Course_Id", "dbo.Courses", "Id");
        }
    }
}
