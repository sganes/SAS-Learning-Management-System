namespace SAS_LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Modules", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.Activities", "Module_Id", "dbo.Modules");
            DropIndex("dbo.Activities", new[] { "Module_Id" });
            DropIndex("dbo.Modules", new[] { "Course_Id" });
            RenameColumn(table: "dbo.Modules", name: "Course_Id", newName: "CourseId");
            RenameColumn(table: "dbo.Activities", name: "Module_Id", newName: "ModuleId");
            AlterColumn("dbo.Activities", "ModuleId", c => c.Int(nullable: false));
            AlterColumn("dbo.Modules", "CourseId", c => c.Int(nullable: false));
            CreateIndex("dbo.Activities", "ModuleId");
            CreateIndex("dbo.Modules", "CourseId");
            AddForeignKey("dbo.Modules", "CourseId", "dbo.Courses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Activities", "ModuleId", "dbo.Modules", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Activities", "ModuleId", "dbo.Modules");
            DropForeignKey("dbo.Modules", "CourseId", "dbo.Courses");
            DropIndex("dbo.Modules", new[] { "CourseId" });
            DropIndex("dbo.Activities", new[] { "ModuleId" });
            AlterColumn("dbo.Modules", "CourseId", c => c.Int());
            AlterColumn("dbo.Activities", "ModuleId", c => c.Int());
            RenameColumn(table: "dbo.Activities", name: "ModuleId", newName: "Module_Id");
            RenameColumn(table: "dbo.Modules", name: "CourseId", newName: "Course_Id");
            CreateIndex("dbo.Modules", "Course_Id");
            CreateIndex("dbo.Activities", "Module_Id");
            AddForeignKey("dbo.Activities", "Module_Id", "dbo.Modules", "Id");
            AddForeignKey("dbo.Modules", "Course_Id", "dbo.Courses", "Id");
        }
    }
}
