namespace Coursework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Contracts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TotalAmount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Estimates",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        FullPrice = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UnitOfMeasurement = c.String(),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Users", "Salt", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Salt");
            DropTable("dbo.Services");
            DropTable("dbo.Estimates");
            DropTable("dbo.Contracts");
            DropTable("dbo.Clients");
        }
    }
}
