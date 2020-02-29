namespace ShortUrl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Removerequiredatshorturl : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Links", "ShortUrl", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Links", "ShortUrl", c => c.String(nullable: false));
        }
    }
}
