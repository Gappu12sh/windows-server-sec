namespace Customer.Data.Application.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatingPODDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("app.PurchaseOrderDetails", "POD_DOE", c => c.DateTime());
            AddColumn("app.PurchaseOrderDetails", "POD_DOU", c => c.DateTime());
            DropColumn("app.PurchaseOrderDetails", "PO_DOE");
            DropColumn("app.PurchaseOrderDetails", "PO_DOU");
        }
        
        public override void Down()
        {
            AddColumn("app.PurchaseOrderDetails", "PO_DOU", c => c.DateTime());
            AddColumn("app.PurchaseOrderDetails", "PO_DOE", c => c.DateTime());
            DropColumn("app.PurchaseOrderDetails", "POD_DOU");
            DropColumn("app.PurchaseOrderDetails", "POD_DOE");
        }
    }
}
