namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seenAddedDb4 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ViewedPosts", "Seen");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ViewedPosts", "Seen", c => c.Boolean(nullable: false));
        }
    }
}
