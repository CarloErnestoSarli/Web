namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seenAddedDb3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "ViewedPost_ViewedId", "dbo.ViewedPosts");
            DropIndex("dbo.AspNetUsers", new[] { "ViewedPost_ViewedId" });
            AddColumn("dbo.ViewedPosts", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.ViewedPosts", "User_Id");
            AddForeignKey("dbo.ViewedPosts", "User_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.AspNetUsers", "ViewedPost_ViewedId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "ViewedPost_ViewedId", c => c.Int());
            DropForeignKey("dbo.ViewedPosts", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ViewedPosts", new[] { "User_Id" });
            DropColumn("dbo.ViewedPosts", "User_Id");
            CreateIndex("dbo.AspNetUsers", "ViewedPost_ViewedId");
            AddForeignKey("dbo.AspNetUsers", "ViewedPost_ViewedId", "dbo.ViewedPosts", "ViewedId");
        }
    }
}
