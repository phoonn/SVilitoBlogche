using DataModel.Models.Identity;
using Microsoft.AspNet.Identity;

namespace DataModel.Identity.ManagerAndStore
{
    public class AppUserManager : UserManager<User, int>
    {
        public AppUserManager(IUserStore<User, int> store) : base(store)
        {
        }
    }
}