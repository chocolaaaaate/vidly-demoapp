namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDoBToCustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "DoB", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "DoB");
        }
    }
}
