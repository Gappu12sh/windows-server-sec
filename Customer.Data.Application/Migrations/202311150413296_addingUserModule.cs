namespace Customer.Data.Application.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingUserModule : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "app.Modules",
                c => new
                    {
                        ModuleId = c.Int(nullable: false, identity: true),
                        ModuleName = c.String(),
                        ModuleUrl = c.String(),
                    })
                .PrimaryKey(t => t.ModuleId);
            
            CreateTable(
                "app.UserModulePermissions",
                c => new
                    {
                        UserModulePermissionId = c.Int(nullable: false, identity: true),
                        ModuleId = c.Int(),
                        UserId = c.Int(),
                        IsAdd = c.Boolean(),
                        IsDeleted = c.Boolean(),
                        IsEdit = c.Boolean(),
                        IsView = c.Boolean(),
                    })
                .PrimaryKey(t => t.UserModulePermissionId);
            
        }
        
        public override void Down()
        {
            DropTable("app.UserModulePermissions");
            DropTable("app.Modules");
        }
    }
}
