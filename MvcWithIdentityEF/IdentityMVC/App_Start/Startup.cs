using BusinessLogic;
using DataAcess.Repositories;
using DataModel.Identity;
using DataModel.Identity.ManagerAndStore;
using Interfaces.BusinessLogic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Web.Http;
using System.Web.Mvc;
using Unity;

namespace IdentityMVC.App_Start
{
    public class Startup
    {
        public static Func<AppUserManager> UserManagerFactory { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions

            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/auth/login"),
            });

            UnityConfig.ConfigureUnityInj();
            
            app.CreatePerOwinContext(() => UnityConfig.GetContainer().Resolve<IUsersLogic>());

            // configure the user manager
            UserManagerFactory = () =>
            {
                var usermanager = new AppUserManager(
                    new AppUserStore(new MyIdentityAppContext()));
                // allow alphanumeric characters in username
                usermanager.UserValidator = new UserValidator<User,int>(usermanager)
                {
                    AllowOnlyAlphanumericUserNames = false
                };

                return usermanager;
            };

        }
    }
}