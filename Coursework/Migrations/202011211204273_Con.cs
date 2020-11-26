namespace Coursework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Con : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ServiceEstimates",
                c => new
                    {
                        Service_ID = c.Int(nullable: false),
                        Estimate_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Service_ID, t.Estimate_ID })
                .ForeignKey("dbo.Services", t => t.Service_ID, cascadeDelete: true)
                .ForeignKey("dbo.Estimates", t => t.Estimate_ID, cascadeDelete: true)
                .Index(t => t.Service_ID)
                .Index(t => t.Estimate_ID);
            
            AddColumn("dbo.Contracts", "DateConclusionContract", c => c.DateTime(nullable: false));
            AddColumn("dbo.Contracts", "DateOfCompletion", c => c.DateTime(nullable: false));
            AddColumn("dbo.Contracts", "ClientID", c => c.Int());
            AddColumn("dbo.Estimates", "CurrentContractID", c => c.Int(nullable: false));
            CreateIndex("dbo.Contracts", "ClientID");
            CreateIndex("dbo.Estimates", "CurrentContractID");
            AddForeignKey("dbo.Contracts", "ClientID", "dbo.Clients", "ID");
            AddForeignKey("dbo.Estimates", "CurrentContractID", "dbo.Contracts", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Estimates", "CurrentContractID", "dbo.Contracts");
            DropForeignKey("dbo.ServiceEstimates", "Estimate_ID", "dbo.Estimates");
            DropForeignKey("dbo.ServiceEstimates", "Service_ID", "dbo.Services");
            DropForeignKey("dbo.Contracts", "ClientID", "dbo.Clients");
            DropIndex("dbo.ServiceEstimates", new[] { "Estimate_ID" });
            DropIndex("dbo.ServiceEstimates", new[] { "Service_ID" });
            DropIndex("dbo.Estimates", new[] { "CurrentContractID" });
            DropIndex("dbo.Contracts", new[] { "ClientID" });
            DropColumn("dbo.Estimates", "CurrentContractID");
            DropColumn("dbo.Contracts", "ClientID");
            DropColumn("dbo.Contracts", "DateOfCompletion");
            DropColumn("dbo.Contracts", "DateConclusionContract");
            DropTable("dbo.ServiceEstimates");
        }
    }
}
