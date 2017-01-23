namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'067cb406-d48e-4688-817f-2de193730a3a', N'guest@vidly.com', 0, N'AIPEYEtywJVBIinPpIJyAt9tqXht6M77Wb3LHMjXEQwkWUMKa23BY0671FjuZfQJ3Q==', N'46b033c2-dfca-4476-ac4b-7e2ac2f05408', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'a36cb469-8684-434b-a98e-35ef672536e1', N'admin@vidly.com', 0, N'AEvpPLeY9Zo9X8cE0cI4drhQ+OBAbuZj+IlBvKPkudvFKjrTWr2u/CVujjZ6dPYzuw==', N'47f6a42a-adfe-4234-9e26-54b801f8b7a2', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')

                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'27e90ef5-3318-456b-b35b-674f8a4dfd3c', N'CanManageMovies')

                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a36cb469-8684-434b-a98e-35ef672536e1', N'27e90ef5-3318-456b-b35b-674f8a4dfd3c')"
            );
        }
        
        public override void Down()
        {
        }
    }
}
