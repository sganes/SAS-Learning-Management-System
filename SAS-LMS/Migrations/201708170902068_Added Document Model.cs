namespace SAS_LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDocumentModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DocName = c.String(),
                        DocPath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Documents");
        }
    }
}
