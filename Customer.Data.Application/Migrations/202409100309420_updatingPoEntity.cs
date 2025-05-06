namespace Customer.Data.Application.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatingPoEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("app.PurchaseOrder", "PartyName", c => c.String());
            AddColumn("app.PurchaseOrder", "PartyAddress", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("app.PurchaseOrder", "PartyAddress");
            DropColumn("app.PurchaseOrder", "PartyName");
        }
    }
}
