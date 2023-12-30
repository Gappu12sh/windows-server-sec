namespace Customer.Data.Application.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class partyContactEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "app.Lookup",
                c => new
                    {
                        LookupId = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Type = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.LookupId);
            
            CreateTable(
                "app.PartAddress",
                c => new
                    {
                        PartyAddress_Id = c.Int(nullable: false, identity: true),
                        Party_Id = c.Int(nullable: false),
                        Contact_Email = c.String(),
                        Contact_Phone = c.String(),
                        Address_Type = c.Int(nullable: false),
                        Address_Line1 = c.String(),
                        Address_Line2 = c.String(),
                        CountryId = c.Int(),
                        StateId = c.Int(),
                        CityId = c.Int(),
                        GST = c.String(),
                        Supplier_Address = c.Boolean(nullable: false),
                        Contact_Remark = c.String(),
                        Pincode = c.String(),
                        Representative_Id = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        PartyAddress_Created_By = c.Int(),
                        PartyAddress_Updated_By = c.Int(),
                        PartyAddress_DOE = c.DateTime(),
                        PartyAddress_DOU = c.DateTime(),
                    })
                .PrimaryKey(t => t.PartyAddress_Id)
                .ForeignKey("app.Lookup", t => t.Address_Type, cascadeDelete: true)
                .ForeignKey("app.Lookup", t => t.CityId)
                .ForeignKey("app.Lookup", t => t.CountryId)
                .ForeignKey("app.PartyMaster", t => t.Party_Id, cascadeDelete: true)
                .ForeignKey("app.Representatives", t => t.Representative_Id, cascadeDelete: true)
                .ForeignKey("app.Lookup", t => t.StateId)
                .ForeignKey("app.UserDetails", t => t.PartyAddress_Created_By)
                .ForeignKey("app.UserDetails", t => t.PartyAddress_Updated_By)
                .Index(t => t.Party_Id)
                .Index(t => t.Address_Type)
                .Index(t => t.CountryId)
                .Index(t => t.StateId)
                .Index(t => t.CityId)
                .Index(t => t.Representative_Id)
                .Index(t => t.PartyAddress_Created_By)
                .Index(t => t.PartyAddress_Updated_By);
            
            CreateTable(
                "app.PartyMaster",
                c => new
                    {
                        Party_Id = c.Int(nullable: false, identity: true),
                        Party_Name = c.String(),
                        Manufacturer_Type = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        Party_Created_By = c.Int(),
                        Party_Updated_By = c.Int(),
                        Party_DOE = c.DateTime(),
                        Party_DOU = c.DateTime(),
                    })
                .PrimaryKey(t => t.Party_Id)
                .ForeignKey("app.UserDetails", t => t.Party_Created_By)
                .ForeignKey("app.UserDetails", t => t.Party_Updated_By)
                .Index(t => t.Party_Created_By)
                .Index(t => t.Party_Updated_By);
            
            CreateTable(
                "app.PartyEmail",
                c => new
                    {
                        PartyEmail_Id = c.Int(nullable: false, identity: true),
                        Party_Id = c.Int(nullable: false),
                        Email_Type = c.Int(),
                        Email = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        PartyEmail_Created_By = c.Int(),
                        PartyEmail_Updated_By = c.Int(),
                        PartyEmail_DOE = c.DateTime(),
                        PartyEmail_DOU = c.DateTime(),
                    })
                .PrimaryKey(t => t.PartyEmail_Id)
                .ForeignKey("app.Lookup", t => t.Email_Type)
                .ForeignKey("app.PartyMaster", t => t.Party_Id, cascadeDelete: true)
                .ForeignKey("app.UserDetails", t => t.PartyEmail_Created_By)
                .ForeignKey("app.UserDetails", t => t.PartyEmail_Updated_By)
                .Index(t => t.Party_Id)
                .Index(t => t.Email_Type)
                .Index(t => t.PartyEmail_Created_By)
                .Index(t => t.PartyEmail_Updated_By);
            
            CreateTable(
                "app.PartyPhone",
                c => new
                    {
                        PartyPhone_Id = c.Int(nullable: false, identity: true),
                        Party_Id = c.Int(nullable: false),
                        Phone_Type = c.Int(),
                        Phone_Number = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        PartyPhone_Created_By = c.Int(),
                        PartyPhone_Updated_By = c.Int(),
                        PartyPhone_DOE = c.DateTime(),
                        PartyPhone_DOU = c.DateTime(),
                    })
                .PrimaryKey(t => t.PartyPhone_Id)
                .ForeignKey("app.PartyMaster", t => t.Party_Id, cascadeDelete: true)
                .ForeignKey("app.Lookup", t => t.Phone_Type)
                .ForeignKey("app.UserDetails", t => t.PartyPhone_Created_By)
                .ForeignKey("app.UserDetails", t => t.PartyPhone_Updated_By)
                .Index(t => t.Party_Id)
                .Index(t => t.Phone_Type)
                .Index(t => t.PartyPhone_Created_By)
                .Index(t => t.PartyPhone_Updated_By);
            
            CreateIndex("app.ProductApplicationUsage", "Product_Id");
            AddForeignKey("app.ProductApplicationUsage", "Product_Id", "app.Product", "Product_Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("app.ProductApplicationUsage", "Product_Id", "app.Product");
            DropForeignKey("app.PartyPhone", "PartyPhone_Updated_By", "app.UserDetails");
            DropForeignKey("app.PartyPhone", "PartyPhone_Created_By", "app.UserDetails");
            DropForeignKey("app.PartyPhone", "Phone_Type", "app.Lookup");
            DropForeignKey("app.PartyPhone", "Party_Id", "app.PartyMaster");
            DropForeignKey("app.PartyEmail", "PartyEmail_Updated_By", "app.UserDetails");
            DropForeignKey("app.PartyEmail", "PartyEmail_Created_By", "app.UserDetails");
            DropForeignKey("app.PartyEmail", "Party_Id", "app.PartyMaster");
            DropForeignKey("app.PartyEmail", "Email_Type", "app.Lookup");
            DropForeignKey("app.PartAddress", "PartyAddress_Updated_By", "app.UserDetails");
            DropForeignKey("app.PartAddress", "PartyAddress_Created_By", "app.UserDetails");
            DropForeignKey("app.PartAddress", "StateId", "app.Lookup");
            DropForeignKey("app.PartAddress", "Representative_Id", "app.Representatives");
            DropForeignKey("app.PartAddress", "Party_Id", "app.PartyMaster");
            DropForeignKey("app.PartyMaster", "Party_Updated_By", "app.UserDetails");
            DropForeignKey("app.PartyMaster", "Party_Created_By", "app.UserDetails");
            DropForeignKey("app.PartAddress", "CountryId", "app.Lookup");
            DropForeignKey("app.PartAddress", "CityId", "app.Lookup");
            DropForeignKey("app.PartAddress", "Address_Type", "app.Lookup");
            DropIndex("app.ProductApplicationUsage", new[] { "Product_Id" });
            DropIndex("app.PartyPhone", new[] { "PartyPhone_Updated_By" });
            DropIndex("app.PartyPhone", new[] { "PartyPhone_Created_By" });
            DropIndex("app.PartyPhone", new[] { "Phone_Type" });
            DropIndex("app.PartyPhone", new[] { "Party_Id" });
            DropIndex("app.PartyEmail", new[] { "PartyEmail_Updated_By" });
            DropIndex("app.PartyEmail", new[] { "PartyEmail_Created_By" });
            DropIndex("app.PartyEmail", new[] { "Email_Type" });
            DropIndex("app.PartyEmail", new[] { "Party_Id" });
            DropIndex("app.PartyMaster", new[] { "Party_Updated_By" });
            DropIndex("app.PartyMaster", new[] { "Party_Created_By" });
            DropIndex("app.PartAddress", new[] { "PartyAddress_Updated_By" });
            DropIndex("app.PartAddress", new[] { "PartyAddress_Created_By" });
            DropIndex("app.PartAddress", new[] { "Representative_Id" });
            DropIndex("app.PartAddress", new[] { "CityId" });
            DropIndex("app.PartAddress", new[] { "StateId" });
            DropIndex("app.PartAddress", new[] { "CountryId" });
            DropIndex("app.PartAddress", new[] { "Address_Type" });
            DropIndex("app.PartAddress", new[] { "Party_Id" });
            DropTable("app.PartyPhone");
            DropTable("app.PartyEmail");
            DropTable("app.PartyMaster");
            DropTable("app.PartAddress");
            DropTable("app.Lookup");
        }
    }
}
