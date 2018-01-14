using DataModel.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityMVC.Models.Identity
{
    public class MyIdentityAppContext : IdentityDbContext<User>
    {
        public MyIdentityAppContext () : base("DbConnectionString")
        {

        }
    }
}