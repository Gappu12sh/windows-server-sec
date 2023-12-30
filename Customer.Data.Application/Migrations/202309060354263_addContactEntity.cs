namespace Customer.Data.Application.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addContactEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "app.Contacts",
                c => new
                    {
                        Contact_Id = c.Int(nullable: false, identity: true),
                        Party_Id = c.Int(nullable: false),
                        PartyAddress_Id = c.Int(),
                        ContactTitle = c.String(),
                        ContactPersonName = c.String(),
                        Designation = c.String(),
                        DateOfBirth = c.DateTime(),
                        DateOfAnniversary = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        Contact_Created_By = c.Int(),
                        Contact_Updated_By = c.Int(),
                        Contact_DOE = c.DateTime(),
                        Contact_DOU = c.DateTime(),
                    })
                .PrimaryKey(t => t.Contact_Id)
                .ForeignKey("app.PartyMaster", t => t.Party_Id, cascadeDelete: true)
                .ForeignKey("app.PartAddress", t => t.PartyAddress_Id)
                .ForeignKey("app.UserDetails", t => t.Contact_Created_By)
                .ForeignKey("app.UserDetails", t => t.Contact_Updated_By)
                .Index(t => t.Party_Id)
                .Index(t => t.PartyAddress_Id)
                .Index(t => t.Contact_Created_By)
                .Index(t => t.Contact_Updated_By);
            
        }
        
        public override void Down()
        {
            DropForeignKey("app.Contacts", "Contact_Updated_By", "app.UserDetails");
            DropForeignKey("app.Contacts", "Contact_Created_By", "app.UserDetails");
            DropForeignKey("app.Contacts", "PartyAddress_Id", "app.PartAddress");
            DropForeignKey("app.Contacts", "Party_Id", "app.PartyMaster");
            DropIndex("app.Contacts", new[] { "Contact_Updated_By" });
            DropIndex("app.Contacts", new[] { "Contact_Created_By" });
            DropIndex("app.Contacts", new[] { "PartyAddress_Id" });
            DropIndex("app.Contacts", new[] { "Party_Id" });
            DropTable("app.Contacts");
        }
    }
}
