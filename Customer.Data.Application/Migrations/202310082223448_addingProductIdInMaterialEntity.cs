namespace Customer.Data.Application.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingProductIdInMaterialEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("app.Material", "ProductId", c => c.Int());
            CreateIndex("app.Material", "ProductId");
            AddForeignKey("app.Material", "ProductId", "app.Product", "Product_Id");
        }
        
        public override void Down()
        {
            DropForeignKey("app.Material", "ProductId", "app.Product");
            DropIndex("app.Material", new[] { "ProductId" });
            DropColumn("app.Material", "ProductId");
        }
    }
}
