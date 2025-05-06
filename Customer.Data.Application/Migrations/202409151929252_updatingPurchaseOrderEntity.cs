namespace Customer.Data.Application.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatingPurchaseOrderEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("app.PurchaseOrder", "CourierChg", c => c.String());
            AddColumn("app.PurchaseOrder", "PaymentTerms", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("app.PurchaseOrder", "PaymentTerms");
            DropColumn("app.PurchaseOrder", "CourierChg");
        }
    }
}
