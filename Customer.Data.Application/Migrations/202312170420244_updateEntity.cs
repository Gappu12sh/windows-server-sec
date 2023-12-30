namespace Customer.Data.Application.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("app.UserPermissions", "IsActive", c => c.Boolean(nullable: false));
            CreateIndex("app.UserPermissions", "ActionId");
            AddForeignKey("app.UserPermissions", "ActionId", "app.Lookup", "LookupId");
        }
        
        public override void Down()
        {
            DropForeignKey("app.UserPermissions", "ActionId", "app.Lookup");
            DropIndex("app.UserPermissions", new[] { "ActionId" });
            DropColumn("app.UserPermissions", "IsActive");
        }
    }
}
