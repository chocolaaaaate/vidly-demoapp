namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeSmalldatetimeToDatetimeMoviesTable : DbMigration
    {
        public override void Up()
        {
            Sql("ALTER TABLE Movies ALTER COLUMN ReleaseDate DATETIME");
            Sql("ALTER TABLE Movies ALTER COLUMN DateAdded DATETIME");
        }
        
        public override void Down()
        {
        }
    }
}
