using Microsoft.AspNet.Identity;

namespace IdentityMVC.Models.Identity.ManagerAndStore
{
    public class AppUserManager : UserManager<User, int>
    {
        public AppUserManager(IUserStore<User, int> store) : base(store)
        {
        }
    }
}