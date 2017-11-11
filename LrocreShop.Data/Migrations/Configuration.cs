namespace LrocreShop.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Model.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Diagnostics;
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

            CreateProductCategorySample(context);
            CreateUser(context);
            CreateSlideSample(context);
            CreatePage(context);

        }
        private void CreateUser(LrocreShopDbContext context)
        {

            //var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new LrocreShopDbContext()));

            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new LrocreShopDbContext()));

            //var user = new ApplicationUser()
            //{
            //    UserName = "tphan",
            //    Email = "tphan.dev@gmail.com",
            //    EmailConfirmed = true,
            //    BirthDay = DateTime.Now,
            //    FullName = "Phan Tran"

            //};

            //manager.Create(user, "12345678@X");

            //if (!roleManager.Roles.Any())
            //{
            //    roleManager.Create(new IdentityRole { Name = "Admin" });
            //    roleManager.Create(new IdentityRole { Name = "User" });
            //}

            //var adminUser = manager.FindByEmail("tphan.dev@gmail.com");

            //manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
        }
        private void CreateProductCategorySample(LrocreShop.Data.LrocreShopDbContext context)
        {
            if (context.ProductCategories.Count() == 0)
            {
                List<ProductCategory> listProductCategory = new List<ProductCategory>() {
                new ProductCategory() {Name="Kem trị mụn", Alias="Kem-tri-mun",Status=true },
                new ProductCategory() {Name="Kem trắng da", Alias="Kem-trang-da",Status=true },
                new ProductCategory() {Name="Nước hoa", Alias="nuoc-hoa",Status=true },
                new ProductCategory() {Name="Thực phẩm giảm cân",Alias="thuc-pham-giam-can",Status=true }
                         };
                context.ProductCategories.AddRange(listProductCategory);
                context.SaveChanges();
            }
        }
        private void CreateSlideSample(LrocreShopDbContext context)
        {
            if (context.Slides.Count() == 0)
            {
                List<Slide> listSlide = new List<Slide>() {
                new Slide() {Name="Slide 1", DisplayOrder=1, Url="#",Image="/Assets/client/images/bag1.jpg",Status=true,Content=@"<h2>FLAT 50% 0FF</h2>
                <label>FOR ALL PURCHASE <b>VALUE</b></label>
                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et </p>
                <span class=""on-get"">GET NOW</span>" },
                new Slide() {Name="Slide 2", DisplayOrder=1, Url="#",Image="/Assets/client/images/bag.jpg",Status=true,Content=@"<h2>FLAT 50% 0FF</h2>
                <label>FOR ALL PURCHASE <b>VALUE</b></label>
                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et </p>
                <span class=""on-get"">GET NOW</span>" },
                new Slide() {Name="Slide 3", DisplayOrder=1, Url="#",Image="/Assets/client/images/baa.jpg",Status=true },
               
                         };
                context.Slides.AddRange(listSlide);
                context.SaveChanges();
            }
        }

        private void CreatePage(LrocreShopDbContext context)
        {
            if (context.Pages.Count() == 0)
            {
                try
                {
                    var page = new Page()
                    {
                        Name = "Giới thiệu",
                        Alias = "gioi-thieu",
                        Content = @"Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium ",
                        Status = true

                    };
                    context.Pages.Add(page);
                    context.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var eve in ex.EntityValidationErrors)
                    {
                        Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                        }
                    }
                }

            }
        }
    }
}
