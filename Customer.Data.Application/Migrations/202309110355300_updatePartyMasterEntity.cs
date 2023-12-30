namespace Customer.Data.Application.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatePartyMasterEntity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("app.PartyEmail", "Party_Id", "app.PartyMaster");
            DropForeignKey("app.PartyPhone", "Party_Id", "app.PartyMaster");
            DropIndex("app.PartyEmail", new[] { "Party_Id" });
            DropIndex("app.PartyPhone", new[] { "Party_Id" });
            AddColumn("app.PartyEmail", "Address_Id", c => c.Int());
            AddColumn("app.PartyPhone", "Address_Id", c => c.Int());
            CreateIndex("app.PartyEmail", "Address_Id");
            CreateIndex("app.PartyPhone", "Address_Id");
            AddForeignKey("app.PartyEmail", "Address_Id", "app.PartAddress", "PartyAddress_Id");
            AddForeignKey("app.PartyPhone", "Address_Id", "app.PartAddress", "PartyAddress_Id");
            DropColumn("app.PartyEmail", "Party_Id");
            DropColumn("app.PartyPhone", "Party_Id");
        }
        
        public override void Down()
        {
            AddColumn("app.PartyPhone", "Party_Id", c => c.Int(nullable: false));
            AddColumn("app.PartyEmail", "Party_Id", c => c.Int(nullable: false));
            DropForeignKey("app.PartyPhone", "Address_Id", "app.PartAddress");
            DropForeignKey("app.PartyEmail", "Address_Id", "app.PartAddress");
            DropIndex("app.PartyPhone", new[] { "Address_Id" });
            DropIndex("app.PartyEmail", new[] { "Address_Id" });
            DropColumn("app.PartyPhone", "Address_Id");
            DropColumn("app.PartyEmail", "Address_Id");
            CreateIndex("app.PartyPhone", "Party_Id");
            CreateIndex("app.PartyEmail", "Party_Id");
            AddForeignKey("app.PartyPhone", "Party_Id", "app.PartyMaster", "Party_Id", cascadeDelete: true);
            AddForeignKey("app.PartyEmail", "Party_Id", "app.PartyMaster", "Party_Id", cascadeDelete: true);
        }
    }
}
