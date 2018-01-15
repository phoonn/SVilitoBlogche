using IdentityMVC.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
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