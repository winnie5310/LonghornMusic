namespace LonghornMusic.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using LonghornMusic.DAL;
    using LonghornMusic.Infrastructure;
    using Microsoft.AspNet.Identity.EntityFramework;
    using LonghornMusic.Models;
    using Microsoft.AspNet.Identity;

    internal sealed class Configuration : DbMigrationsConfiguration<LonghornMusic.DAL.AppIdentityDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(LonghornMusic.DAL.AppIdentityDbContext context)
        {
            AddUser(context, "Managers", "Admin", "MySecret", "admin@example.com");
            AddUser(context, "Users", "Bob", "MySecret", "bob@example.com");
            AddUser(context, "Users", "Alice", "MySecret", "alice@example.com");
            AddUser(context, "Employee", "Employee", "MySecret", "employee@example.com");
        }

        void AddUser(AppIdentityDbContext context, string roleName, string userName, string password, string email)
        {
            AppUserManager userMgr = new AppUserManager(new UserStore<AppUser>(context));
            AppRoleManager roleMgr = new AppRoleManager(new RoleStore<AppRole>(context));


            if (!roleMgr.RoleExists(roleName))
            {
                roleMgr.Create(new AppRole(roleName));
            }

            AppUser user = userMgr.FindByName(userName);
            if (user == null)
            {
                userMgr.Create(new AppUser { UserName = userName, Email = email },
                    password);
                user = userMgr.FindByName(userName);
            }

            if (!userMgr.IsInRole(user.Id, roleName))
            {
                userMgr.AddToRole(user.Id, roleName);
            }


            context.SaveChanges();
        
        }
    }
}
