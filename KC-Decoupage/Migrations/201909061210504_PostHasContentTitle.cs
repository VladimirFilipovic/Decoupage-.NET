namespace KC_Decoupage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostHasContentTitle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Title", c => c.String());
            AddColumn("dbo.Posts", "Content", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Content");
            DropColumn("dbo.Posts", "Title");
        }
    }
}
