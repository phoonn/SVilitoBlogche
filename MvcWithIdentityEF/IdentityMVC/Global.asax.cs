using DataModel.Identity;
using DataModel.Identity.ManagerAndStore;
using IdentityMVC.App_Start;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace IdentityMVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            UnityConfig.ConfigureUnityInj();

            MyIdentityAppContext context = new MyIdentityAppContext();
            context.Database.CreateIfNotExists();
            if (context.Roles.FirstOrDefault(i=>i.Name=="User")==null)
            {
                context.Roles.Add(new AppRole("User"));
                context.Roles.Add(new AppRole("Administrator"));
                context.SaveChanges();
            }
            context.Dispose();
        }
    }
}
