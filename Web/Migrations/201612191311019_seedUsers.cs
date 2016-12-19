namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'b1892b6a-8698-4b63-8f7a-e9bc60739352', NULL, 0, N'ACw6odP3/o/U3saR6t7p50GbMLBMbVtokSqbtD8R2GhZfe+0V0Bv+q3ZLOe1Ifz62A==', N'ed5c3575-19be-4550-8569-c62ec96b9852', NULL, 0, 0, NULL, 0, 0, N'user1@email.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'c62aec6d-816e-425c-9ad1-3461c265f465', N'lecturer1@email.com', 0, N'AIrHvR+tH7sDUbC4PNi3BgbwB08PTVFOUmIfChjhJk3FL2F9jwuDdsOnhQr6etS8tQ==', N'9ab1752f-f087-428c-a9c4-38ad419d559d', NULL, 0, 0, NULL, 1, 0, N'lecturer1@email.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'adc991ff-9b86-4ba5-a744-d4a0b46cefc1', N'Lecturer')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c62aec6d-816e-425c-9ad1-3461c265f465', N'adc991ff-9b86-4ba5-a744-d4a0b46cefc1')

");
        }
        
        public override void Down()
        {
        }
    }
}
