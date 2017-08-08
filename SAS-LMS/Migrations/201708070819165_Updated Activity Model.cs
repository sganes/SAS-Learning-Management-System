namespace SAS_LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedActivityModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Activities", "Module_Id", "dbo.Modules");
            DropIndex("dbo.Activities", new[] { "Module_Id" });
            RenameColumn(table: "dbo.Activities", name: "Module_Id", newName: "ModuleId");
            AlterColumn("dbo.Activities", "ModuleId", c => c.Int(nullable: false));
            CreateIndex("dbo.Activities", "ModuleId");
            AddForeignKey("dbo.Activities", "ModuleId", "dbo.Modules", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Activities", "ModuleId", "dbo.Modules");
            DropIndex("dbo.Activities", new[] { "ModuleId" });
            AlterColumn("dbo.Activities", "ModuleId", c => c.Int());
            RenameColumn(table: "dbo.Activities", name: "ModuleId", newName: "Module_Id");
            CreateIndex("dbo.Activities", "Module_Id");
            AddForeignKey("dbo.Activities", "Module_Id", "dbo.Modules", "Id");
        }
    }
}
