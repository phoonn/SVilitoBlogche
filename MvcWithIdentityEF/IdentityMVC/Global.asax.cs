using DataAcess.Identity;
using DataModel.Models.Identity;
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

            MyIdentityAppContext context = new MyIdentityAppContext();
            context.Database.CreateIfNotExists();
            if (context.Roles.FirstOrDefault(i=>i.Name=="User")==null)
            {
                context.Roles.Add(new AppRole("User"));
                context.SaveChanges();
            }
            context.Dispose();
        }
    }
}
