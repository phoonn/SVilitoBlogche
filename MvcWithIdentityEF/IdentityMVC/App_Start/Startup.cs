using DataModel.Identity;
using IdentityMVC.Models.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;

namespace IdentityMVC.App_Start
{
    public class Startup
    {
        public static Func<UserManager<User>> UserManagerFactory { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions

            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/auth/login")
            });

            //UnityConfig.ConfigureUnityInj();
            //app.CreatePerOwinContext(() => UnityConfig.GetContainer().Resolve<CrudServiceOf_Laptops_LaptopDTOClient>());

            // configure the user manager
            UserManagerFactory = () =>
            {
                var usermanager = new UserManager<User>(
                    new UserStore<User>(new MyIdentityAppContext()));
                // allow alphanumeric characters in username
                usermanager.UserValidator = new UserValidator<User>(usermanager)
                {
                    AllowOnlyAlphanumericUserNames = false
                };

                return usermanager;
            };

        }
    }
}