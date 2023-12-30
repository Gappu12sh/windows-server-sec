namespace Customer.Data.Application.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatingUserDetailsEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("app.UserDetails", "User_Dept", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("app.UserDetails", "User_Dept");
        }
    }
}
