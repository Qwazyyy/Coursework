namespace Coursework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnDeleteForService : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Services", "Delete", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Services", "Delete");
        }
    }
}
