namespace KC_Decoupage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostWithoutUserModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.Posts", "Creator_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Post_Id1", "dbo.Posts");
            DropForeignKey("dbo.Users", "Post_Id2", "dbo.Posts");
            DropIndex("dbo.Users", new[] { "Post_Id" });
            DropIndex("dbo.Posts", new[] { "Creator_Id" });
            DropIndex("dbo.Users", new[] { "Post_Id1" });
            DropIndex("dbo.Users", new[] { "Post_Id2" });
            AddColumn("dbo.Posts", "Creator", c => c.String());
            DropColumn("dbo.Posts", "Creator_Id");
            DropTable("dbo.Users");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        Post_Id = c.Int(),
                        Post_Id1 = c.Int(),
                        Post_Id2 = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Posts", "Creator_Id", c => c.Int());
            DropColumn("dbo.Posts", "Creator");
            CreateIndex("dbo.Users", "Post_Id2");
            CreateIndex("dbo.Users", "Post_Id1");
            CreateIndex("dbo.Posts", "Creator_Id");
            CreateIndex("dbo.Users", "Post_Id");
            AddForeignKey("dbo.Users", "Post_Id2", "dbo.Posts", "Id");
            AddForeignKey("dbo.Users", "Post_Id1", "dbo.Posts", "Id");
            AddForeignKey("dbo.Posts", "Creator_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Users", "Post_Id", "dbo.Posts", "Id");
        }
    }
}
