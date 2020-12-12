namespace Coursework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newbase : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ServiceEstimates", "Service_ID", "dbo.Services");
            DropForeignKey("dbo.ServiceEstimates", "Estimate_ID", "dbo.Estimates");
            DropIndex("dbo.ServiceEstimates", new[] { "Service_ID" });
            DropIndex("dbo.ServiceEstimates", new[] { "Estimate_ID" });
            AddColumn("dbo.Estimates", "ServiceID", c => c.Int(nullable: false));
            AlterColumn("dbo.Estimates", "Quantity", c => c.Double(nullable: false));
            CreateIndex("dbo.Estimates", "ServiceID");
            AddForeignKey("dbo.Estimates", "ServiceID", "dbo.Services", "ID", cascadeDelete: true);
            DropColumn("dbo.Estimates", "FullPrice");
            DropTable("dbo.ServiceEstimates");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ServiceEstimates",
                c => new
                    {
                        Service_ID = c.Int(nullable: false),
                        Estimate_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Service_ID, t.Estimate_ID });
            
            AddColumn("dbo.Estimates", "FullPrice", c => c.Int(nullable: false));
            DropForeignKey("dbo.Estimates", "ServiceID", "dbo.Services");
            DropIndex("dbo.Estimates", new[] { "ServiceID" });
            AlterColumn("dbo.Estimates", "Quantity", c => c.Int(nullable: false));
            DropColumn("dbo.Estimates", "ServiceID");
            CreateIndex("dbo.ServiceEstimates", "Estimate_ID");
            CreateIndex("dbo.ServiceEstimates", "Service_ID");
            AddForeignKey("dbo.ServiceEstimates", "Estimate_ID", "dbo.Estimates", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ServiceEstimates", "Service_ID", "dbo.Services", "ID", cascadeDelete: true);
        }
    }
}
