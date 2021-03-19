namespace KC_Decoupage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostWithoutArrays : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "LikedBy", c => c.String());
            AddColumn("dbo.Posts", "DislikedBy", c => c.String());
            AddColumn("dbo.Posts", "Comments", c => c.String());
            AddColumn("dbo.Posts", "Commentators", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Commentators");
            DropColumn("dbo.Posts", "Comments");
            DropColumn("dbo.Posts", "DislikedBy");
            DropColumn("dbo.Posts", "LikedBy");
        }
    }
}
