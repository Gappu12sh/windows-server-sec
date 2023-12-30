namespace Customer.Data.Application.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingMaterialEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "app.MaterialRates",
                c => new
                    {
                        MaterialRateId = c.Int(nullable: false, identity: true),
                        MaterialId = c.Int(nullable: false),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OldRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CFY_Id = c.Int(),
                        MaterialRate_Created_By = c.Int(),
                        WEF = c.DateTime(),
                        MaterialRate_DOE = c.DateTime(),
                    })
                .PrimaryKey(t => t.MaterialRateId)
                .ForeignKey("app.FinancialYear", t => t.CFY_Id)
                .ForeignKey("app.UserDetails", t => t.MaterialRate_Created_By)
                .ForeignKey("app.Material", t => t.MaterialId, cascadeDelete: true)
                .Index(t => t.MaterialId)
                .Index(t => t.CFY_Id)
                .Index(t => t.MaterialRate_Created_By);
            
            CreateTable(
                "app.Material",
                c => new
                    {
                        MaterialId = c.Int(nullable: false, identity: true),
                        PartyName = c.String(),
                        ProductName = c.String(),
                        ActualCode = c.String(),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TradeDiscount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OldRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsActive = c.Boolean(nullable: false),
                        PartyId = c.Int(),
                        CFY_Id = c.Int(),
                        Material_Created_By = c.Int(),
                        Material_Updated_By = c.Int(),
                        WEF = c.DateTime(),
                        Material_DOE = c.DateTime(),
                        Material_DOU = c.DateTime(),
                    })
                .PrimaryKey(t => t.MaterialId)
                .ForeignKey("app.FinancialYear", t => t.CFY_Id)
                .ForeignKey("app.PartyMaster", t => t.PartyId)
                .ForeignKey("app.UserDetails", t => t.Material_Created_By)
                .ForeignKey("app.UserDetails", t => t.Material_Updated_By)
                .Index(t => t.PartyId)
                .Index(t => t.CFY_Id)
                .Index(t => t.Material_Created_By)
                .Index(t => t.Material_Updated_By);
            
        }
        
        public override void Down()
        {
            DropForeignKey("app.Material", "Material_Updated_By", "app.UserDetails");
            DropForeignKey("app.Material", "Material_Created_By", "app.UserDetails");
            DropForeignKey("app.Material", "PartyId", "app.PartyMaster");
            DropForeignKey("app.MaterialRates", "MaterialId", "app.Material");
            DropForeignKey("app.Material", "CFY_Id", "app.FinancialYear");
            DropForeignKey("app.MaterialRates", "MaterialRate_Created_By", "app.UserDetails");
            DropForeignKey("app.MaterialRates", "CFY_Id", "app.FinancialYear");
            DropIndex("app.Material", new[] { "Material_Updated_By" });
            DropIndex("app.Material", new[] { "Material_Created_By" });
            DropIndex("app.Material", new[] { "CFY_Id" });
            DropIndex("app.Material", new[] { "PartyId" });
            DropIndex("app.MaterialRates", new[] { "MaterialRate_Created_By" });
            DropIndex("app.MaterialRates", new[] { "CFY_Id" });
            DropIndex("app.MaterialRates", new[] { "MaterialId" });
            DropTable("app.Material");
            DropTable("app.MaterialRates");
        }
    }
}
