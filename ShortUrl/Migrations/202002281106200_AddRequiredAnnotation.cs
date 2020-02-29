namespace ShortUrl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequiredAnnotation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Links", "ShortUrl", c => c.String(nullable: false));
            AlterColumn("dbo.Links", "FullUrl", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Links", "FullUrl", c => c.String());
            AlterColumn("dbo.Links", "ShortUrl", c => c.String());
        }
    }
}
