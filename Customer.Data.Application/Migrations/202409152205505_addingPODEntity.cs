namespace Customer.Data.Application.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingPODEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("app.PurchaseOrderDetails", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("app.PurchaseOrderDetails", "POD_Created_By", c => c.Int());
            AddColumn("app.PurchaseOrderDetails", "POD_Updated_By", c => c.Int());
            CreateIndex("app.PurchaseOrderDetails", "POD_Created_By");
            CreateIndex("app.PurchaseOrderDetails", "POD_Updated_By");
            AddForeignKey("app.PurchaseOrderDetails", "POD_Created_By", "app.UserDetails", "UserID");
            AddForeignKey("app.PurchaseOrderDetails", "POD_Updated_By", "app.UserDetails", "UserID");
        }
        
        public override void Down()
        {
            DropForeignKey("app.PurchaseOrderDetails", "POD_Updated_By", "app.UserDetails");
            DropForeignKey("app.PurchaseOrderDetails", "POD_Created_By", "app.UserDetails");
            DropIndex("app.PurchaseOrderDetails", new[] { "POD_Updated_By" });
            DropIndex("app.PurchaseOrderDetails", new[] { "POD_Created_By" });
            DropColumn("app.PurchaseOrderDetails", "POD_Updated_By");
            DropColumn("app.PurchaseOrderDetails", "POD_Created_By");
            DropColumn("app.PurchaseOrderDetails", "IsActive");
        }
    }
}
