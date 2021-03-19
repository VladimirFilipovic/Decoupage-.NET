namespace KC_Decoupage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostValidationPart1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Posts", "Content", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "Content", c => c.String());
            AlterColumn("dbo.Posts", "Title", c => c.String());
        }
    }
}
