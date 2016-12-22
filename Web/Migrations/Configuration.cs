namespace Web.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Web.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Web.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Web.Models.ApplicationDbContext context)
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
            AddUsers(context);
            
        }

        void AddUsers(Web.Models.ApplicationDbContext context)
        {
            var user1 = new ApplicationUser { UserName = "Student1@email.com",
                Email = "Student1@email.com",
                Name = "john",
                Surname = "whatever",
            };
            var um = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            um.Create(user1, "password");

            
            var user2 = new ApplicationUser { UserName = "Student2@email.com",
                Email = "Student2@email.com",
                Name = "ed",
                Surname = "harper",
            };
            um.Create(user2, "password");

            var user3 = new ApplicationUser { UserName = "Student3@email.com",
                Email = "Student3@email.com",
                Name = "tyler",
                Surname = "gordon",
            };
            um.Create(user3, "password");

            var user4 = new ApplicationUser { UserName = "Student4@email.com",
                Email = "Student4@email.com",
                Name = "tom",
                Surname = "phelps",
            };
            um.Create(user4, "password");

            var user5 = new ApplicationUser { UserName = "Student5@email.com",
                Email = "Student5@email.com",
                Name = "nemo",
                Surname = "doe",
            };
            um.Create(user5, "password");

            var user6 = new ApplicationUser { UserName = "Lecturer@email.com",
                Email = "Lecturer@email.com",
                Name = "Sean",
                Surname = "Walton",
            };
            um.Create(user6, "password");

            var userId = um.FindByName(user6.UserName);

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            if (!roleManager.RoleExists(RoleName.Lecturer))
            {
                roleManager.Create(new IdentityRole(RoleName.Lecturer));
            }
            um.AddToRole(userId.Id, RoleName.Lecturer);
        }

       
            
        

        
       
    }
}
