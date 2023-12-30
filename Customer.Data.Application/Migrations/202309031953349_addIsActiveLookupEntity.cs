namespace Customer.Data.Application.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIsActiveLookupEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("app.Lookup", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("app.Lookup", "IsActive");
        }
    }
}
