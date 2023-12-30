namespace Customer.Data.Application.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addGstCertificateFieldPartyAddressEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("app.PartAddress", "Zone", c => c.String());
            AddColumn("app.PartAddress", "GstCertificate", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("app.PartAddress", "GstCertificate");
            DropColumn("app.PartAddress", "Zone");
        }
    }
}
