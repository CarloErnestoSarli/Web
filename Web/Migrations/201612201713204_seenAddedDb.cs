namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seenAddedDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ViewedPosts",
                c => new
                    {
                        ViewedId = c.Int(nullable: false, identity: true),
                        AnnouncementId = c.Int(nullable: false),
                        Seen = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ViewedId);
            
            AddColumn("dbo.AspNetUsers", "ViewedPost_ViewedId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "ViewedPost_ViewedId");
            AddForeignKey("dbo.AspNetUsers", "ViewedPost_ViewedId", "dbo.ViewedPosts", "ViewedId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "ViewedPost_ViewedId", "dbo.ViewedPosts");
            DropIndex("dbo.AspNetUsers", new[] { "ViewedPost_ViewedId" });
            DropColumn("dbo.AspNetUsers", "ViewedPost_ViewedId");
            DropTable("dbo.ViewedPosts");
        }
    }
}
