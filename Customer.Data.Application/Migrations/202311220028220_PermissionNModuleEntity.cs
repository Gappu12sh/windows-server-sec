namespace Customer.Data.Application.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PermissionNModuleEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("app.Modules", "IsActive", c => c.Boolean(nullable: false));
            AlterColumn("app.UserModulePermissions", "IsAdd", c => c.Boolean(nullable: false));
            AlterColumn("app.UserModulePermissions", "IsDeleted", c => c.Boolean(nullable: false));
            AlterColumn("app.UserModulePermissions", "IsEdit", c => c.Boolean(nullable: false));
            AlterColumn("app.UserModulePermissions", "IsView", c => c.Boolean(nullable: false));
            CreateIndex("app.UserModulePermissions", "ModuleId");
            CreateIndex("app.UserModulePermissions", "UserId");
            AddForeignKey("app.UserModulePermissions", "ModuleId", "app.Modules", "ModuleId");
            AddForeignKey("app.UserModulePermissions", "UserId", "app.UserDetails", "UserID");
        }
        
        public override void Down()
        {
            DropForeignKey("app.UserModulePermissions", "UserId", "app.UserDetails");
            DropForeignKey("app.UserModulePermissions", "ModuleId", "app.Modules");
            DropIndex("app.UserModulePermissions", new[] { "UserId" });
            DropIndex("app.UserModulePermissions", new[] { "ModuleId" });
            AlterColumn("app.UserModulePermissions", "IsView", c => c.Boolean());
            AlterColumn("app.UserModulePermissions", "IsEdit", c => c.Boolean());
            AlterColumn("app.UserModulePermissions", "IsDeleted", c => c.Boolean());
            AlterColumn("app.UserModulePermissions", "IsAdd", c => c.Boolean());
            DropColumn("app.Modules", "IsActive");
        }
    }
}
