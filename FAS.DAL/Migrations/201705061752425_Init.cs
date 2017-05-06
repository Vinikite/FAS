namespace FAS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        MiddleName = c.String(),
                        IdAddress = c.Guid(),
                        AverageIncome = c.Double(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifyOn = c.DateTime(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.IdAddress)
                .Index(t => t.IdAddress)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        RoleId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifyOn = c.DateTime(),
                        Country = c.String(nullable: false),
                        City = c.String(nullable: false),
                        Street = c.String(nullable: false),
                        House = c.String(nullable: false),
                        Flat = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Banks",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifyOn = c.DateTime(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifyOn = c.DateTime(),
                        Name = c.String(nullable: false),
                        Notation = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Scores",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifyOn = c.DateTime(),
                        IdUser = c.Guid(nullable: false),
                        IdViewScore = c.Guid(nullable: false),
                        IdTypeScore = c.Guid(nullable: false),
                        IdStatus = c.Guid(nullable: false),
                        Balance = c.Double(nullable: false),
                        Notation = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.IdUser, cascadeDelete: true)
                .ForeignKey("dbo.ViewScores", t => t.IdViewScore)
                .ForeignKey("dbo.TypeScores", t => t.IdTypeScore)
                .ForeignKey("dbo.Statusses", t => t.IdStatus)
                .Index(t => t.IdUser)
                .Index(t => t.IdViewScore)
                .Index(t => t.IdTypeScore)
                .Index(t => t.IdStatus);
            
            CreateTable(
                "dbo.Statusses",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifyOn = c.DateTime(),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifyOn = c.DateTime(),
                        IdTransactionType = c.Guid(nullable: false),
                        IdScore = c.Guid(nullable: false),
                        IdCategory = c.Guid(nullable: false),
                        IdBank = c.Guid(nullable: false),
                        Comission = c.Double(nullable: false),
                        Notation = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TransactionTypes", t => t.IdTransactionType)
                .ForeignKey("dbo.Scores", t => t.IdScore)
                .ForeignKey("dbo.Categories", t => t.IdCategory)
                .ForeignKey("dbo.Banks", t => t.IdBank)
                .Index(t => t.IdTransactionType)
                .Index(t => t.IdScore)
                .Index(t => t.IdCategory)
                .Index(t => t.IdBank);
            
            CreateTable(
                "dbo.TransactionTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifyOn = c.DateTime(),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TypeScores",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifyOn = c.DateTime(),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ViewScores",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifyOn = c.DateTime(),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "IdBank", "dbo.Banks");
            DropForeignKey("dbo.Transactions", "IdCategory", "dbo.Categories");
            DropForeignKey("dbo.Transactions", "IdScore", "dbo.Scores");
            DropForeignKey("dbo.Transactions", "IdTransactionType", "dbo.TransactionTypes");
            DropForeignKey("dbo.Scores", "IdStatus", "dbo.Statusses");
            DropForeignKey("dbo.Scores", "IdTypeScore", "dbo.TypeScores");
            DropForeignKey("dbo.Scores", "IdViewScore", "dbo.ViewScores");
            DropForeignKey("dbo.Scores", "IdUser", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "IdAddress", "dbo.Addresses");
            DropIndex("dbo.Transactions", new[] { "IdBank" });
            DropIndex("dbo.Transactions", new[] { "IdCategory" });
            DropIndex("dbo.Transactions", new[] { "IdScore" });
            DropIndex("dbo.Transactions", new[] { "IdTransactionType" });
            DropIndex("dbo.Scores", new[] { "IdStatus" });
            DropIndex("dbo.Scores", new[] { "IdTypeScore" });
            DropIndex("dbo.Scores", new[] { "IdViewScore" });
            DropIndex("dbo.Scores", new[] { "IdUser" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "IdAddress" });
            DropTable("dbo.ViewScores");
            DropTable("dbo.TypeScores");
            DropTable("dbo.TransactionTypes");
            DropTable("dbo.Transactions");
            DropTable("dbo.Statusses");
            DropTable("dbo.Scores");
            DropTable("dbo.Categories");
            DropTable("dbo.Banks");
            DropTable("dbo.Addresses");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
        }
    }
}
