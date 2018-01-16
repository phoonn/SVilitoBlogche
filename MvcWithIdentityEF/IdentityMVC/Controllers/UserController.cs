using DataModel.Models.Identity;
using DataModel.Identity.ManagerAndStore;
using Interfaces.Repositories;
using System.Web.Mvc;
using IdentityMVC.App_Start;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using IdentityMVC.Models;

namespace IdentityMVC.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UserController : BaseController
    {
        private IUserRepository Repository;
        private AppUserManager UserManager;

        public UserController(IUserRepository Repository)
        {
            this.Repository = Repository;
            UserManager = Startup.UserManagerFactory.Invoke();
        }

        public async Task<ActionResult> Index(int page = 1)
        {
            IEnumerable<User> Users;
            int PageSize = 10;
            int TotalCount = await Repository.CountUsersAsync();
            Users = Repository.Get(null, o => o.OrderBy(i => i.Id), "Roles", PageSize,(PageSize*(page-1)));
            int pageCount = (TotalCount % PageSize == 0 ? TotalCount / PageSize : (TotalCount / PageSize + 1));
            var result = new PagedListModel<User>(Users, TotalCount, PageSize);
            
            return View(result);
        }
    }
}