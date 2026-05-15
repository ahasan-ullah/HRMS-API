namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upadtedsalaryrevisiontable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SalaryRevisions", "RevisionDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SalaryRevisions", "RevisionDate", c => c.DateTime(nullable: false));
        }
    }
}
