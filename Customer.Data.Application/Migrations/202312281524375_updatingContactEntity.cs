namespace Customer.Data.Application.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatingContactEntity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("app.EmailLists", "PartyId", "app.PartyMaster");
            DropForeignKey("app.EmailLists", "PartyAddressId", "app.PartAddress");
            DropForeignKey("app.PhoneLists", "PartyId", "app.PartyMaster");
            DropForeignKey("app.PhoneLists", "PartyAddressId", "app.PartAddress");
            DropForeignKey("app.Contacts", "Party_Id", "app.PartyMaster");
            DropIndex("app.Contacts", new[] { "Party_Id" });
            DropIndex("app.EmailLists", new[] { "PartyId" });
            DropIndex("app.EmailLists", new[] { "PartyAddressId" });
            DropIndex("app.PhoneLists", new[] { "PartyId" });
            DropIndex("app.PhoneLists", new[] { "PartyAddressId" });
            AlterColumn("app.Contacts", "Party_Id", c => c.Int());
            CreateIndex("app.Contacts", "Party_Id");
            AddForeignKey("app.Contacts", "Party_Id", "app.PartyMaster", "Party_Id");
        }
        
        public override void Down()
        {
            DropForeignKey("app.Contacts", "Party_Id", "app.PartyMaster");
            DropIndex("app.Contacts", new[] { "Party_Id" });
            AlterColumn("app.Contacts", "Party_Id", c => c.Int(nullable: false));
            CreateIndex("app.PhoneLists", "PartyAddressId");
            CreateIndex("app.PhoneLists", "PartyId");
            CreateIndex("app.EmailLists", "PartyAddressId");
            CreateIndex("app.EmailLists", "PartyId");
            CreateIndex("app.Contacts", "Party_Id");
            AddForeignKey("app.Contacts", "Party_Id", "app.PartyMaster", "Party_Id", cascadeDelete: true);
            AddForeignKey("app.PhoneLists", "PartyAddressId", "app.PartAddress", "PartyAddress_Id");
            AddForeignKey("app.PhoneLists", "PartyId", "app.PartyMaster", "Party_Id");
            AddForeignKey("app.EmailLists", "PartyAddressId", "app.PartAddress", "PartyAddress_Id");
            AddForeignKey("app.EmailLists", "PartyId", "app.PartyMaster", "Party_Id");
        }
    }
}
