namespace KC_Decoupage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FileBypass : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "ImagePath", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "ImagePath", c => c.String());
        }
    }
}
