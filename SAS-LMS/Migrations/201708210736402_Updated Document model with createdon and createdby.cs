namespace SAS_LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedDocumentmodelwithcreatedonandcreatedby : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Documents", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Documents", "CreatedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Documents", "CreatedBy");
            DropColumn("dbo.Documents", "CreatedDate");
        }
    }
}
