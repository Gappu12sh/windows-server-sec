namespace Customer.Data.Application.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingPartyIdPOEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("app.PurchaseOrder", "PartyId", c => c.Int());
            AddColumn("app.PurchaseOrder", "PartyAddressId", c => c.Int());
            AddColumn("app.PurchaseOrder", "SampleNumberId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("app.PurchaseOrder", "SampleNumberId");
            DropColumn("app.PurchaseOrder", "PartyAddressId");
            DropColumn("app.PurchaseOrder", "PartyId");
        }
    }
}
