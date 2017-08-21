namespace SAS_LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RebuildDatabaseagain : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Documents", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Documents", "Description", c => c.String());
        }
    }
}
