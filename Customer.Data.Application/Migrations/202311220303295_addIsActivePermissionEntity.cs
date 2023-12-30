namespace Customer.Data.Application.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIsActivePermissionEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("app.UserModulePermissions", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("app.UserModulePermissions", "IsActive");
        }
    }
}
