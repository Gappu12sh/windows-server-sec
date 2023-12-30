namespace Customer.Data.Application.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletingExtraFieldMaterialEntity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("app.Material", "CFY_Id", "app.FinancialYear");
            DropIndex("app.Material", new[] { "CFY_Id" });
            DropColumn("app.Material", "Rate");
            DropColumn("app.Material", "OldRate");
            DropColumn("app.Material", "CFY_Id");
            DropColumn("app.Material", "WEF");
        }
        
        public override void Down()
        {
            AddColumn("app.Material", "WEF", c => c.DateTime());
            AddColumn("app.Material", "CFY_Id", c => c.Int());
            AddColumn("app.Material", "OldRate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("app.Material", "Rate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            CreateIndex("app.Material", "CFY_Id");
            AddForeignKey("app.Material", "CFY_Id", "app.FinancialYear", "FinancialYear_Id");
        }
    }
}
