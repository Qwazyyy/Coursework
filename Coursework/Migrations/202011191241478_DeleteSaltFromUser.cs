namespace Coursework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteSaltFromUser : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "Salt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Salt", c => c.String());
        }
    }
}
