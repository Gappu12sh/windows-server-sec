namespace Customer.Data.Application.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingNewEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "app.ApplicationUsage",
                c => new
                    {
                        ApplicationUsage_Id = c.Int(nullable: false, identity: true),
                        ApplicationUsage_Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        ApplicationUsage_Created_By = c.Int(),
                        ApplicationUsage_Updated_By = c.Int(),
                        ApplicationUsage_DOE = c.DateTime(),
                        ApplicationUsage_DOU = c.DateTime(),
                    })
                .PrimaryKey(t => t.ApplicationUsage_Id)
                .ForeignKey("app.UserDetails", t => t.ApplicationUsage_Created_By)
                .ForeignKey("app.UserDetails", t => t.ApplicationUsage_Updated_By)
                .Index(t => t.ApplicationUsage_Created_By)
                .Index(t => t.ApplicationUsage_Updated_By);
            
            CreateTable(
                "app.UserDetails",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        User_Email = c.String(),
                        User_Password = c.String(),
                        User_Created_By = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                        User_DOE = c.DateTime(),
                        User_Updated_By = c.Int(),
                        User_DOU = c.DateTime(),
                        User_Type = c.String(),
                        Rep_ID = c.Int(),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("app.Representatives", t => t.Rep_ID)
                .ForeignKey("app.UserDetails", t => t.User_Created_By)
                .ForeignKey("app.UserDetails", t => t.User_Updated_By)
                .Index(t => t.User_Created_By)
                .Index(t => t.User_Updated_By)
                .Index(t => t.Rep_ID);
            
            CreateTable(
                "app.Representatives",
                c => new
                    {
                        Rep_ID = c.Int(nullable: false, identity: true),
                        Rep_Unique_ID = c.Int(nullable: false),
                        Rep_Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        Rep_Created_By = c.Int(),
                        Rep_Updated_By = c.Int(),
                        Rep_DOE = c.DateTime(),
                        Rep_DOU = c.DateTime(),
                    })
                .PrimaryKey(t => t.Rep_ID)
                .ForeignKey("app.UserDetails", t => t.Rep_Created_By)
                .ForeignKey("app.UserDetails", t => t.Rep_Updated_By)
                .Index(t => t.Rep_Created_By)
                .Index(t => t.Rep_Updated_By);
            
            CreateTable(
                "app.DepartmentMaster",
                c => new
                    {
                        Department_ID = c.Int(nullable: false, identity: true),
                        Department_Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        Department_Created_By = c.Int(),
                        Department_Updated_By = c.Int(),
                        Department_DOE = c.DateTime(),
                        Department_DOU = c.DateTime(),
                    })
                .PrimaryKey(t => t.Department_ID)
                .ForeignKey("app.UserDetails", t => t.Department_Created_By)
                .ForeignKey("app.UserDetails", t => t.Department_Updated_By)
                .Index(t => t.Department_Created_By)
                .Index(t => t.Department_Updated_By);
            
            CreateTable(
                "app.FinancialYear",
                c => new
                    {
                        FinancialYear_Id = c.Int(nullable: false, identity: true),
                        Financial_Year = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        FinancialYear_Created_By = c.Int(),
                        FinancialYear_Updated_By = c.Int(),
                        FinancialYear_DOE = c.DateTime(),
                        FinancialYear_DOU = c.DateTime(),
                    })
                .PrimaryKey(t => t.FinancialYear_Id)
                .ForeignKey("app.UserDetails", t => t.FinancialYear_Created_By)
                .ForeignKey("app.UserDetails", t => t.FinancialYear_Updated_By)
                .Index(t => t.FinancialYear_Created_By)
                .Index(t => t.FinancialYear_Updated_By);
            
            CreateTable(
                "app.Group",
                c => new
                    {
                        GroupId = c.Int(nullable: false, identity: true),
                        Group_Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        Group_Created_By = c.Int(),
                        Group_DOE = c.DateTime(),
                    })
                .PrimaryKey(t => t.GroupId)
                .ForeignKey("app.UserDetails", t => t.Group_Created_By)
                .Index(t => t.Group_Created_By);
            
            CreateTable(
                "app.LogInSession",
                c => new
                    {
                        LoginSessionId = c.Int(nullable: false, identity: true),
                        LoginToken = c.String(nullable: false),
                        LoginUserId = c.Int(nullable: false),
                        SessionTimeStamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.LoginSessionId)
                .ForeignKey("app.UserDetails", t => t.LoginUserId, cascadeDelete: true)
                .Index(t => t.LoginUserId);
            
            CreateTable(
                "app.Product",
                c => new
                    {
                        Product_Id = c.Int(nullable: false, identity: true),
                        Product_Name = c.String(),
                        Product_Price = c.String(),
                        Product_UpdateOn = c.DateTime(nullable: false),
                        FinancialYear_Id = c.Int(nullable: false),
                        Product_Group = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        Product_Created_By = c.Int(),
                        Product_Updated_By = c.Int(),
                        Product_DOE = c.DateTime(),
                        Product_DOU = c.DateTime(),
                    })
                .PrimaryKey(t => t.Product_Id)
                .ForeignKey("app.FinancialYear", t => t.FinancialYear_Id, cascadeDelete: true)
                .ForeignKey("app.UserDetails", t => t.Product_Created_By)
                .ForeignKey("app.UserDetails", t => t.Product_Updated_By)
                .Index(t => t.FinancialYear_Id)
                .Index(t => t.Product_Created_By)
                .Index(t => t.Product_Updated_By);
            
            CreateTable(
                "app.ProductApplicationUsage",
                c => new
                    {
                        ProductApplicationUsage_Id = c.Int(nullable: false, identity: true),
                        Product_Id = c.Int(nullable: false),
                        ApplicationUsage_Id = c.Int(nullable: false),
                        Product_Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        FinancialYear_DOE = c.DateTime(),
                    })
                .PrimaryKey(t => t.ProductApplicationUsage_Id)
                .ForeignKey("app.ApplicationUsage", t => t.ApplicationUsage_Id, cascadeDelete: true)
                .Index(t => t.ApplicationUsage_Id);
            
            CreateTable(
                "app.TermCondition",
                c => new
                    {
                        TermCondition_Id = c.Int(nullable: false, identity: true),
                        Term = c.String(),
                    })
                .PrimaryKey(t => t.TermCondition_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("app.ProductApplicationUsage", "ApplicationUsage_Id", "app.ApplicationUsage");
            DropForeignKey("app.Product", "Product_Updated_By", "app.UserDetails");
            DropForeignKey("app.Product", "Product_Created_By", "app.UserDetails");
            DropForeignKey("app.Product", "FinancialYear_Id", "app.FinancialYear");
            DropForeignKey("app.LogInSession", "LoginUserId", "app.UserDetails");
            DropForeignKey("app.Group", "Group_Created_By", "app.UserDetails");
            DropForeignKey("app.FinancialYear", "FinancialYear_Updated_By", "app.UserDetails");
            DropForeignKey("app.FinancialYear", "FinancialYear_Created_By", "app.UserDetails");
            DropForeignKey("app.DepartmentMaster", "Department_Updated_By", "app.UserDetails");
            DropForeignKey("app.DepartmentMaster", "Department_Created_By", "app.UserDetails");
            DropForeignKey("app.ApplicationUsage", "ApplicationUsage_Updated_By", "app.UserDetails");
            DropForeignKey("app.ApplicationUsage", "ApplicationUsage_Created_By", "app.UserDetails");
            DropForeignKey("app.UserDetails", "User_Updated_By", "app.UserDetails");
            DropForeignKey("app.UserDetails", "User_Created_By", "app.UserDetails");
            DropForeignKey("app.UserDetails", "Rep_ID", "app.Representatives");
            DropForeignKey("app.Representatives", "Rep_Updated_By", "app.UserDetails");
            DropForeignKey("app.Representatives", "Rep_Created_By", "app.UserDetails");
            DropIndex("app.ProductApplicationUsage", new[] { "ApplicationUsage_Id" });
            DropIndex("app.Product", new[] { "Product_Updated_By" });
            DropIndex("app.Product", new[] { "Product_Created_By" });
            DropIndex("app.Product", new[] { "FinancialYear_Id" });
            DropIndex("app.LogInSession", new[] { "LoginUserId" });
            DropIndex("app.Group", new[] { "Group_Created_By" });
            DropIndex("app.FinancialYear", new[] { "FinancialYear_Updated_By" });
            DropIndex("app.FinancialYear", new[] { "FinancialYear_Created_By" });
            DropIndex("app.DepartmentMaster", new[] { "Department_Updated_By" });
            DropIndex("app.DepartmentMaster", new[] { "Department_Created_By" });
            DropIndex("app.Representatives", new[] { "Rep_Updated_By" });
            DropIndex("app.Representatives", new[] { "Rep_Created_By" });
            DropIndex("app.UserDetails", new[] { "Rep_ID" });
            DropIndex("app.UserDetails", new[] { "User_Updated_By" });
            DropIndex("app.UserDetails", new[] { "User_Created_By" });
            DropIndex("app.ApplicationUsage", new[] { "ApplicationUsage_Updated_By" });
            DropIndex("app.ApplicationUsage", new[] { "ApplicationUsage_Created_By" });
            DropTable("app.TermCondition");
            DropTable("app.ProductApplicationUsage");
            DropTable("app.Product");
            DropTable("app.LogInSession");
            DropTable("app.Group");
            DropTable("app.FinancialYear");
            DropTable("app.DepartmentMaster");
            DropTable("app.Representatives");
            DropTable("app.UserDetails");
            DropTable("app.ApplicationUsage");
        }
    }
}
