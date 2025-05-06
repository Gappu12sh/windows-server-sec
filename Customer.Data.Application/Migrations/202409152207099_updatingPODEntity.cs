namespace Customer.Data.Application.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatingPODEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("app.PurchaseOrderDetails", "PO_DOE", c => c.DateTime());
            AddColumn("app.PurchaseOrderDetails", "PO_DOU", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("app.PurchaseOrderDetails", "PO_DOU");
            DropColumn("app.PurchaseOrderDetails", "PO_DOE");
        }
    }
}
