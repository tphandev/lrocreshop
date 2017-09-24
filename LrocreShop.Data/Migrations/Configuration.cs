namespace LrocreShop.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Model.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LrocreShop.Data.LrocreShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LrocreShop.Data.LrocreShopDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new LrocreShopDbContext()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new LrocreShopDbContext()));

            var user = new ApplicationUser()
            {
                UserName = "tphan",
                Email = "tphan.dev@gmail.com",
                EmailConfirmed = true,
                BirthDay = DateTime.Now,
                FullName = "Phan Tran"

            };

            manager.Create(user, "12345678@X");

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "User" });
            }

            var adminUser = manager.FindByEmail("tphan.dev@gmail.com");

            manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
        }
    }
}
