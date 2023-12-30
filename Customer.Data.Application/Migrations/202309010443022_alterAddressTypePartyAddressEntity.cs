namespace Customer.Data.Application.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterAddressTypePartyAddressEntity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("app.PartAddress", "Address_Type", "app.Lookup");
            DropIndex("app.PartAddress", new[] { "Address_Type" });
            AlterColumn("app.PartAddress", "Address_Type", c => c.Int());
            CreateIndex("app.PartAddress", "Address_Type");
            AddForeignKey("app.PartAddress", "Address_Type", "app.Lookup", "LookupId");
        }
        
        public override void Down()
        {
            DropForeignKey("app.PartAddress", "Address_Type", "app.Lookup");
            DropIndex("app.PartAddress", new[] { "Address_Type" });
            AlterColumn("app.PartAddress", "Address_Type", c => c.Int(nullable: false));
            CreateIndex("app.PartAddress", "Address_Type");
            AddForeignKey("app.PartAddress", "Address_Type", "app.Lookup", "LookupId", cascadeDelete: true);
        }
    }
}
