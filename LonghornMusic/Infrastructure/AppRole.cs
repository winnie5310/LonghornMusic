using Microsoft.AspNet.Identity.EntityFramework;

namespace LonghornMusic.Infrastructure
{
    public class AppRole : IdentityRole
    {
        public AppRole() : base() { }

        public AppRole(string name) : base(name) { }
        
    }
}