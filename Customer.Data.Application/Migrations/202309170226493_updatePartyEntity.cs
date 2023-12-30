namespace Customer.Data.Application.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatePartyEntity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("app.PartyEmail", "Address_Id", "app.PartAddress");
            DropForeignKey("app.PartyPhone", "Address_Id", "app.PartAddress");
            DropIndex("app.PartyEmail", new[] { "Address_Id" });
            DropIndex("app.PartyPhone", new[] { "Address_Id" });
            AddColumn("app.PartyEmail", "Party_Id", c => c.Int());
            AddColumn("app.PartyPhone", "Party_Id", c => c.Int());
            CreateIndex("app.PartyEmail", "Party_Id");
            CreateIndex("app.PartyPhone", "Party_Id");
            AddForeignKey("app.PartyEmail", "Party_Id", "app.PartyMaster", "Party_Id");
            AddForeignKey("app.PartyPhone", "Party_Id", "app.PartyMaster", "Party_Id");
            DropColumn("app.PartyEmail", "Address_Id");
            DropColumn("app.PartyPhone", "Address_Id");
        }
        
        public override void Down()
        {
            AddColumn("app.PartyPhone", "Address_Id", c => c.Int());
            AddColumn("app.PartyEmail", "Address_Id", c => c.Int());
            DropForeignKey("app.PartyPhone", "Party_Id", "app.PartyMaster");
            DropForeignKey("app.PartyEmail", "Party_Id", "app.PartyMaster");
            DropIndex("app.PartyPhone", new[] { "Party_Id" });
            DropIndex("app.PartyEmail", new[] { "Party_Id" });
            DropColumn("app.PartyPhone", "Party_Id");
            DropColumn("app.PartyEmail", "Party_Id");
            CreateIndex("app.PartyPhone", "Address_Id");
            CreateIndex("app.PartyEmail", "Address_Id");
            AddForeignKey("app.PartyPhone", "Address_Id", "app.PartAddress", "PartyAddress_Id");
            AddForeignKey("app.PartyEmail", "Address_Id", "app.PartAddress", "PartyAddress_Id");
        }
    }
}
