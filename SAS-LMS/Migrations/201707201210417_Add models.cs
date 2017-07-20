namespace SAS_LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addmodels : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ActivityTypes", "ActivityId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ActivityTypes", "ActivityId", c => c.Int(nullable: false));
        }
    }
}
