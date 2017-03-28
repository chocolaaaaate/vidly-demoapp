namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changetodatetime2 : DbMigration
    {
        public override void Up()
        {
            Sql("ALTER TABLE Movies ALTER COLUMN ReleaseDate DATETIME2");
            Sql("ALTER TABLE Movies ALTER COLUMN DateAdded DATETIME2");
        }
        
        public override void Down()
        {
        }
    }
}
