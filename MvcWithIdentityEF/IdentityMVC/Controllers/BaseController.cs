using DataModel.Identity;
using System.Security.Claims;
using System.Web.Mvc;

namespace IdentityMVC.Controllers
{
    public class BaseController : Controller
    {
        public AppClaimsPrincipal CurrentUser
        {
            get { return new AppClaimsPrincipal((ClaimsPrincipal)this.User); }
        }


        public BaseController()
        {

        }
    }
}