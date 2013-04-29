 namespace MvcMusicStore.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using WebMatrix.WebData;

    internal sealed class Configuration : DbMigrationsConfiguration<MvcMusicStore.Models.UsersContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MvcMusicStore.Models.UsersContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            WebSecurity.InitializeDatabaseConnection(
                "DefaultConnection",
                "UserProfile",
                "UserId",
                "UserName",
                autoCreateTables: true);

            if (!Roles.RoleExists("Administrator"))
                Roles.CreateRole("Administrator");

            if (!Roles.RoleExists("Customer"))
                Roles.CreateRole("Customer");

            if (!WebSecurity.UserExists("SiteAdmin"))
                WebSecurity.CreateUserAndAccount("SiteAdmin", "password",
                    new { Email = "sadmin@mvcmusicstore.com" });

            if (!WebSecurity.UserExists("steve"))
                WebSecurity.CreateUserAndAccount("steve", "password",
                    new { Email = "steve@gmail.com" });

            if (!Roles.GetRolesForUser("SiteAdmin").Contains("Administrator"))
                Roles.AddUserToRole("SiteAdmin", "Administrator");

            if (!Roles.GetRolesForUser("steve").Contains("Customer"))
                Roles.AddUserToRole("steve", "Customer");
        }
    }
}
