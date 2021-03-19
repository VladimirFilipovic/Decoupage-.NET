namespace KC_Decoupage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class retriveAdmin : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [PasswordHash], [SecurityStamp], [Discriminator]) VALUES (N'a89ea68c-c1cb-4c8c-9df1-77849d5d1f99', N'quest', N'AMhjBjMrHrEtOsCHcJi20fAJyTU0+itWmoepDGFBeLx0W1fy/rjALtTZlISXU3D/Uw==', N'0238c9c0-26df-4ebe-9f23-3dff27921fb0', N'ApplicationUser')
INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [PasswordHash], [SecurityStamp], [Discriminator]) VALUES (N'fa2e7ec6-062a-4855-8230-f15f9094481c', N'admin', N'AFFmL9cEoUqaxjLFjpKPbvRfaX6Qgm2ExTcs70cLUkjThdudMFLdBwX3/ijompYTDQ==', N'58482630-6694-4714-959e-2b8f6c024192', N'ApplicationUser')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'fa2e7ec6-062a-4855-8230-f15f9094481c', N'269f9046-3fdc-4579-8afd-a778cd3f405e')
");
        }
        
        public override void Down()
        {
        }
    }
}
