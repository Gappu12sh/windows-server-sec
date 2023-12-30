namespace Customer.Data.Application.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIsModule : DbMigration
    {
        public override void Up()
        {
            AddColumn("app.UserModulePermissions", "IsModule", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("app.UserModulePermissions", "IsModule");
        }
    }
}
