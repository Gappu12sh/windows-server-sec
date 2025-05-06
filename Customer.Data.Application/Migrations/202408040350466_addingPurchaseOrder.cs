namespace Customer.Data.Application.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingPurchaseOrder : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "app.PurchaseOrder",
                c => new
                    {
                        PurchaseOrderId = c.Int(nullable: false, identity: true),
                        SNo = c.String(),
                        Date = c.DateTime(),
                        PONo = c.String(),
                        PODate = c.DateTime(),
                        GSTNo = c.String(),
                        DispatchCity = c.String(),
                        DispatchVia = c.String(),
                        ExecutiveName = c.String(),
                        Remark = c.String(),
                        MaterialDelAddress = c.String(),
                        PackedBy = c.String(),
                        PackedDate = c.DateTime(),
                        Label = c.String(),
                        Bottle = c.String(),
                        InvoiceNo = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        PO_Created_By = c.Int(),
                        PO_Updated_By = c.Int(),
                        PO_DOE = c.DateTime(),
                        PO_DOU = c.DateTime(),
                    })
                .PrimaryKey(t => t.PurchaseOrderId)
                .ForeignKey("app.UserDetails", t => t.PO_Created_By)
                .ForeignKey("app.UserDetails", t => t.PO_Updated_By)
                .Index(t => t.PO_Created_By)
                .Index(t => t.PO_Updated_By);
            
            CreateTable(
                "app.PurchaseOrderDetails",
                c => new
                    {
                        PurchaseOrderDetailsId = c.Int(nullable: false, identity: true),
                        PurchaseOrderId = c.Int(nullable: false),
                        ItemName = c.String(),
                        Qty = c.String(),
                        Rate = c.String(),
                        Packing = c.String(),
                        TDPer = c.String(),
                        BatchNo = c.String(),
                        InternalCode = c.String(),
                        MfgDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.PurchaseOrderDetailsId)
                .ForeignKey("app.PurchaseOrder", t => t.PurchaseOrderId, cascadeDelete: true)
                .Index(t => t.PurchaseOrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("app.PurchaseOrder", "PO_Updated_By", "app.UserDetails");
            DropForeignKey("app.PurchaseOrder", "PO_Created_By", "app.UserDetails");
            DropForeignKey("app.PurchaseOrderDetails", "PurchaseOrderId", "app.PurchaseOrder");
            DropIndex("app.PurchaseOrderDetails", new[] { "PurchaseOrderId" });
            DropIndex("app.PurchaseOrder", new[] { "PO_Updated_By" });
            DropIndex("app.PurchaseOrder", new[] { "PO_Created_By" });
            DropTable("app.PurchaseOrderDetails");
            DropTable("app.PurchaseOrder");
        }
    }
}
