namespace Customer.Data.Application.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatePermissionEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "app.UserPermissions",
                c => new
                    {
                        UserPermissionId = c.Int(nullable: false, identity: true),
                        UserModulePermissionId = c.Int(nullable: false),
                        ActionId = c.Int(),
                    })
                .PrimaryKey(t => t.UserPermissionId)
                .ForeignKey("app.UserModulePermissions", t => t.UserModulePermissionId, cascadeDelete: true)
                .Index(t => t.UserModulePermissionId);
            
            CreateTable(
                "app.ModuleActions",
                c => new
                    {
                        ModuleId = c.Int(nullable: false),
                        ActionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ModuleId, t.ActionId })
                .ForeignKey("app.Modules", t => t.ModuleId, cascadeDelete: true)
                .ForeignKey("app.Lookup", t => t.ActionId, cascadeDelete: true)
                .Index(t => t.ModuleId)
                .Index(t => t.ActionId);
            
            AddColumn("app.Modules", "ModuleCode", c => c.String());
            DropColumn("app.UserModulePermissions", "IsAdd");
            DropColumn("app.UserModulePermissions", "IsDeleted");
            DropColumn("app.UserModulePermissions", "IsEdit");
            DropColumn("app.UserModulePermissions", "IsView");
        }
        
        public override void Down()
        {
            AddColumn("app.UserModulePermissions", "IsView", c => c.Boolean(nullable: false));
            AddColumn("app.UserModulePermissions", "IsEdit", c => c.Boolean(nullable: false));
            AddColumn("app.UserModulePermissions", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("app.UserModulePermissions", "IsAdd", c => c.Boolean(nullable: false));
            DropForeignKey("app.UserPermissions", "UserModulePermissionId", "app.UserModulePermissions");
            DropForeignKey("app.ModuleActions", "ActionId", "app.Lookup");
            DropForeignKey("app.ModuleActions", "ModuleId", "app.Modules");
            DropIndex("app.ModuleActions", new[] { "ActionId" });
            DropIndex("app.ModuleActions", new[] { "ModuleId" });
            DropIndex("app.UserPermissions", new[] { "UserModulePermissionId" });
            DropColumn("app.Modules", "ModuleCode");
            DropTable("app.ModuleActions");
            DropTable("app.UserPermissions");
        }
    }
}
