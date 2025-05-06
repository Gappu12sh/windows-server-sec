namespace Customer.Data.Application.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatingQuotationEntity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("app.QuotationDetails", "UnitId", "app.Lookup");
            DropIndex("app.QuotationDetails", new[] { "UnitId" });
            RenameColumn(table: "app.QuotationDetails", name: "UnitId", newName: "QuantityId");
            AlterColumn("app.QuotationDetails", "QuantityId", c => c.Int());
            CreateIndex("app.QuotationDetails", "QuantityId");
            AddForeignKey("app.QuotationDetails", "QuantityId", "app.Lookup", "LookupId");
            DropColumn("app.QuotationDetails", "Quantity");
        }
        
        public override void Down()
        {
            AddColumn("app.QuotationDetails", "Quantity", c => c.String());
            DropForeignKey("app.QuotationDetails", "QuantityId", "app.Lookup");
            DropIndex("app.QuotationDetails", new[] { "QuantityId" });
            AlterColumn("app.QuotationDetails", "QuantityId", c => c.Int(nullable: false));
            RenameColumn(table: "app.QuotationDetails", name: "QuantityId", newName: "UnitId");
            CreateIndex("app.QuotationDetails", "UnitId");
            AddForeignKey("app.QuotationDetails", "UnitId", "app.Lookup", "LookupId", cascadeDelete: true);
        }
    }
}
