namespace Customer.Data.Application.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingQuotationEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "app.QuotationDetails",
                c => new
                    {
                        QuotationDetailsId = c.Int(nullable: false, identity: true),
                        QuotationId = c.Int(nullable: false),
                        SampleName = c.String(),
                        ActualName = c.String(),
                        Group = c.String(),
                        Quantity = c.String(),
                        UnitId = c.Int(nullable: false),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GroupId = c.Int(),
                        ActualNameValue = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        QuotationDetails_Created_By = c.Int(),
                        QuotationDetails_Updated_By = c.Int(),
                        Quotation_DOE = c.DateTime(),
                        Quotation_DOU = c.DateTime(),
                    })
                .PrimaryKey(t => t.QuotationDetailsId)
                .ForeignKey("app.QuotationMasters", t => t.QuotationId, cascadeDelete: true)
                .ForeignKey("app.Lookup", t => t.UnitId, cascadeDelete: true)
                .ForeignKey("app.UserDetails", t => t.QuotationDetails_Created_By)
                .ForeignKey("app.UserDetails", t => t.QuotationDetails_Updated_By)
                .Index(t => t.QuotationId)
                .Index(t => t.UnitId)
                .Index(t => t.QuotationDetails_Created_By)
                .Index(t => t.QuotationDetails_Updated_By);
            
            CreateTable(
                "app.QuotationMasters",
                c => new
                    {
                        QuotationMasterId = c.Int(nullable: false, identity: true),
                        QuotationNo = c.String(),
                        RegisterNo = c.String(),
                        PartyId = c.Int(),
                        AddressId = c.Int(nullable: false),
                        QuotationStatus = c.String(),
                        ShippingAddress = c.String(),
                        QuotationRepresentative = c.String(),
                        KindAttTo = c.String(),
                        FinancialYearId = c.Int(nullable: false),
                        ShippingAddressRemarks = c.String(),
                        PartyName = c.String(),
                        ContactId = c.Int(),
                        ContactNo = c.String(),
                        Email = c.String(),
                        Terms = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        Quotation_Created_By = c.Int(),
                        Quotation_Updated_By = c.Int(),
                        Quotation_DOE = c.DateTime(),
                        Quotation_DOU = c.DateTime(),
                    })
                .PrimaryKey(t => t.QuotationMasterId)
                .ForeignKey("app.PartAddress", t => t.AddressId, cascadeDelete: true)
                .ForeignKey("app.Contacts", t => t.ContactId)
                .ForeignKey("app.PartyMaster", t => t.PartyId)
                .ForeignKey("app.UserDetails", t => t.Quotation_Created_By)
                .ForeignKey("app.UserDetails", t => t.Quotation_Updated_By)
                .Index(t => t.PartyId)
                .Index(t => t.AddressId)
                .Index(t => t.ContactId)
                .Index(t => t.Quotation_Created_By)
                .Index(t => t.Quotation_Updated_By);
            
        }
        
        public override void Down()
        {
            DropForeignKey("app.QuotationDetails", "QuotationDetails_Updated_By", "app.UserDetails");
            DropForeignKey("app.QuotationDetails", "QuotationDetails_Created_By", "app.UserDetails");
            DropForeignKey("app.QuotationDetails", "UnitId", "app.Lookup");
            DropForeignKey("app.QuotationMasters", "Quotation_Updated_By", "app.UserDetails");
            DropForeignKey("app.QuotationMasters", "Quotation_Created_By", "app.UserDetails");
            DropForeignKey("app.QuotationDetails", "QuotationId", "app.QuotationMasters");
            DropForeignKey("app.QuotationMasters", "PartyId", "app.PartyMaster");
            DropForeignKey("app.QuotationMasters", "ContactId", "app.Contacts");
            DropForeignKey("app.QuotationMasters", "AddressId", "app.PartAddress");
            DropIndex("app.QuotationMasters", new[] { "Quotation_Updated_By" });
            DropIndex("app.QuotationMasters", new[] { "Quotation_Created_By" });
            DropIndex("app.QuotationMasters", new[] { "ContactId" });
            DropIndex("app.QuotationMasters", new[] { "AddressId" });
            DropIndex("app.QuotationMasters", new[] { "PartyId" });
            DropIndex("app.QuotationDetails", new[] { "QuotationDetails_Updated_By" });
            DropIndex("app.QuotationDetails", new[] { "QuotationDetails_Created_By" });
            DropIndex("app.QuotationDetails", new[] { "UnitId" });
            DropIndex("app.QuotationDetails", new[] { "QuotationId" });
            DropTable("app.QuotationMasters");
            DropTable("app.QuotationDetails");
        }
    }
}
