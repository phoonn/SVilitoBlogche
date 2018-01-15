using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace IdentityMVC.Models.Identity.ManagerAndStore
{
    public interface IAppUserStore: IUserStore<User, int>
    {

    }

    public class AppUserStore : UserStore<User, AppRole, int, AppUserLogin, AppUserRole, AppUserClaim>
    {
        public AppUserStore(DbContext context) : base(context)
        {
        }

        public AppUserStore():base(new MyIdentityAppContext())
        {
        }
    }
}