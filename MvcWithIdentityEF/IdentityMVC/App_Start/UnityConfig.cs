using BusinessLogic;
using DataAcess;
using DataAcess.Repositories;
using DataModel.Identity;
using DataModel.Identity.ManagerAndStore;
using Interfaces.BusinessLogic;
using Interfaces.Repositories;
using System.Data.Entity;
using System.Web.Mvc;
using Unity;
using Unity.Lifetime;

namespace IdentityMVC.App_Start
{
    public class UnityConfig
    {
        private static UnityContainer Container;

        static UnityConfig()
        {
            Container = new UnityContainer();
        }
        public static UnityContainer GetContainer()
        {
            return Container;
        }

        public static void ConfigureUnityInj()
        {

            Container.RegisterType<IUserRepository, UserRepository>();
            Container.RegisterType<IRepository<AppRole>, BaseRepository<AppRole>>();
            Container.RegisterType<IUsersLogic, UsersLogic>();
            Container.RegisterType<IUnitOfWork, UnitOfWork>(new PerResolveLifetimeManager());
            Container.RegisterType<DbContext, MyIdentityAppContext>(new PerResolveLifetimeManager());

            DependencyResolver.SetResolver(new UnityDependencyResolver(Container));
        }
    }
}