namespace Customer.Data.Application.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingQuotationDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("app.QuotationMasters", "QuotationDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("app.QuotationMasters", "QuotationDate");
        }
    }
}
