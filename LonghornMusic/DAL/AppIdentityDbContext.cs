using LonghornMusic.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace LonghornMusic.DAL
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext() : base("StoreContext") { }

        static AppIdentityDbContext()
        {
            Database.SetInitializer<AppIdentityDbContext>(new IdentityDbInit());
        }

        public static AppIdentityDbContext Create()
        {
            return new AppIdentityDbContext();
        }
      
    }

    public class IdentityDbInit : NullDatabaseInitializer<AppIdentityDbContext>
    { }
}