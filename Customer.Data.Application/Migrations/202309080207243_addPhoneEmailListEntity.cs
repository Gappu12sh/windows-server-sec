namespace Customer.Data.Application.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPhoneEmailListEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "app.EmailLists",
                c => new
                    {
                        EmailListId = c.Int(nullable: false, identity: true),
                        PartyId = c.Int(),
                        PartyAddressId = c.Int(),
                        Email_Type = c.Int(),
                        Email = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        EmailList_Created_By = c.Int(),
                        EmailList_Updated_By = c.Int(),
                        EmailList_DOE = c.DateTime(),
                        EmailList_DOU = c.DateTime(),
                    })
                .PrimaryKey(t => t.EmailListId)
                .ForeignKey("app.Lookup", t => t.Email_Type)
                .ForeignKey("app.PartyMaster", t => t.PartyId)
                .ForeignKey("app.PartAddress", t => t.PartyAddressId)
                .ForeignKey("app.UserDetails", t => t.EmailList_Created_By)
                .ForeignKey("app.UserDetails", t => t.EmailList_Updated_By)
                .Index(t => t.PartyId)
                .Index(t => t.PartyAddressId)
                .Index(t => t.Email_Type)
                .Index(t => t.EmailList_Created_By)
                .Index(t => t.EmailList_Updated_By);
            
            CreateTable(
                "app.PhoneLists",
                c => new
                    {
                        PhoneListId = c.Int(nullable: false, identity: true),
                        PartyId = c.Int(),
                        PartyAddressId = c.Int(),
                        Phone_Type = c.Int(),
                        PhoneNumber = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        PhoneList_Created_By = c.Int(),
                        PhoneList_Updated_By = c.Int(),
                        PhoneList_DOE = c.DateTime(),
                        PhoneList_DOU = c.DateTime(),
                    })
                .PrimaryKey(t => t.PhoneListId)
                .ForeignKey("app.PartyMaster", t => t.PartyId)
                .ForeignKey("app.PartAddress", t => t.PartyAddressId)
                .ForeignKey("app.Lookup", t => t.Phone_Type)
                .ForeignKey("app.UserDetails", t => t.PhoneList_Created_By)
                .ForeignKey("app.UserDetails", t => t.PhoneList_Updated_By)
                .Index(t => t.PartyId)
                .Index(t => t.PartyAddressId)
                .Index(t => t.Phone_Type)
                .Index(t => t.PhoneList_Created_By)
                .Index(t => t.PhoneList_Updated_By);
            
        }
        
        public override void Down()
        {
            DropForeignKey("app.PhoneLists", "PhoneList_Updated_By", "app.UserDetails");
            DropForeignKey("app.PhoneLists", "PhoneList_Created_By", "app.UserDetails");
            DropForeignKey("app.PhoneLists", "Phone_Type", "app.Lookup");
            DropForeignKey("app.PhoneLists", "PartyAddressId", "app.PartAddress");
            DropForeignKey("app.PhoneLists", "PartyId", "app.PartyMaster");
            DropForeignKey("app.EmailLists", "EmailList_Updated_By", "app.UserDetails");
            DropForeignKey("app.EmailLists", "EmailList_Created_By", "app.UserDetails");
            DropForeignKey("app.EmailLists", "PartyAddressId", "app.PartAddress");
            DropForeignKey("app.EmailLists", "PartyId", "app.PartyMaster");
            DropForeignKey("app.EmailLists", "Email_Type", "app.Lookup");
            DropIndex("app.PhoneLists", new[] { "PhoneList_Updated_By" });
            DropIndex("app.PhoneLists", new[] { "PhoneList_Created_By" });
            DropIndex("app.PhoneLists", new[] { "Phone_Type" });
            DropIndex("app.PhoneLists", new[] { "PartyAddressId" });
            DropIndex("app.PhoneLists", new[] { "PartyId" });
            DropIndex("app.EmailLists", new[] { "EmailList_Updated_By" });
            DropIndex("app.EmailLists", new[] { "EmailList_Created_By" });
            DropIndex("app.EmailLists", new[] { "Email_Type" });
            DropIndex("app.EmailLists", new[] { "PartyAddressId" });
            DropIndex("app.EmailLists", new[] { "PartyId" });
            DropTable("app.PhoneLists");
            DropTable("app.EmailLists");
        }
    }
}
