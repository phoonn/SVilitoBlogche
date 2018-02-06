using DataModel.Identity.ManagerAndStore;
using System.Web.Mvc;
using IdentityMVC.App_Start;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using IdentityMVC.Models;
using Interfaces.BusinessLogic;
using Microsoft.AspNet.Identity;
using DataModel.Identity;
using BusinessLogic;
using DataAcess.Repositories;
using Interfaces.Repositories;
using DataAcess;

namespace IdentityMVC.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UserController : BaseController
    {
        private IUsersLogic Logic;
        private AppUserManager UserManager;

        public UserController(IUsersLogic Logic)
        {
            this.Logic = Logic;
            UserManager = Startup.UserManagerFactory.Invoke();
        }

        public ActionResult Index(int page = 1)
        {
            IEnumerable<User> Users;
            int PageSize = 10;
            int TotalCount = Logic.CountUsers();
            Users = Logic.GetAll(null, o => o.OrderBy(i => i.Id), "Roles", PageSize,(PageSize*(page-1)));
            int pageCount = (TotalCount % PageSize == 0 ? TotalCount / PageSize : (TotalCount / PageSize + 1));
            var result = new PagedListModel<User>(Users, TotalCount, PageSize);
            
            return View(result);
        }

        public ActionResult Details(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return RedirectToAction("Index");
            }
            var user =  Logic.Find(u => u.UserName == userName);
            return View(user);
        }

        [HttpPost]
        public ActionResult ChangeRole(string userName, string role)
        {
            var user = Logic.Find(u => u.UserName == userName);
            var oldRoleId = user.Roles.SingleOrDefault().RoleId;
            var oldRoleName = Logic.GetRole(oldRoleId).Name;

            if (oldRoleName != role)
            {
                UserManager.RemoveFromRole(user.Id, oldRoleName);
                UserManager.AddToRole(user.Id, role);
                Logic.ModifyUser(user);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ChangeState(string userName, string state)
        {
            bool locked = !state.Equals("0");
            var user = Logic.Find(u => u.UserName == userName);
            user.LockoutEnabled = locked;
            Logic.Edit(user);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(string userName)
        {
            var user = Logic.Find(u => u.UserName == userName);
            Logic.Delete(user);
            return RedirectToAction("Index");
        }
    }
}