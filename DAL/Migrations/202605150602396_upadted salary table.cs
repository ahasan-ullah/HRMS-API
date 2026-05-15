namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upadtedsalarytable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Salaries", "EffectiveDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Salaries", "EffectiveDate", c => c.DateTime(nullable: false));
        }
    }
}
