namespace Customer.Data.Application.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingSampleNoPOEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("app.PurchaseOrder", "SampleNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("app.PurchaseOrder", "SampleNumber");
        }
    }
}
