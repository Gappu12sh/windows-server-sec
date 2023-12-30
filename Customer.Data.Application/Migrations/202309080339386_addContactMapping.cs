namespace Customer.Data.Application.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addContactMapping : DbMigration
    {
        public override void Up()
        {
            AddColumn("app.EmailLists", "ContactId", c => c.Int());
            AddColumn("app.PhoneLists", "ContactId", c => c.Int());
            CreateIndex("app.EmailLists", "ContactId");
            CreateIndex("app.PhoneLists", "ContactId");
            AddForeignKey("app.EmailLists", "ContactId", "app.Contacts", "Contact_Id");
            AddForeignKey("app.PhoneLists", "ContactId", "app.Contacts", "Contact_Id");
        }
        
        public override void Down()
        {
            DropForeignKey("app.PhoneLists", "ContactId", "app.Contacts");
            DropForeignKey("app.EmailLists", "ContactId", "app.Contacts");
            DropIndex("app.PhoneLists", new[] { "ContactId" });
            DropIndex("app.EmailLists", new[] { "ContactId" });
            DropColumn("app.PhoneLists", "ContactId");
            DropColumn("app.EmailLists", "ContactId");
        }
    }
}
