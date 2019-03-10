using Interfaces.BusinessLogic;
using System.Threading.Tasks;
using Interfaces.Repositories;
using DataModel.Identity;

namespace BusinessLogic
{
    public class UsersLogic : BaseCrudLogic<User, IUserRepository>, IUsersLogic 
    {
        
        private IRepository<AppRole> RolesRepo;

        public UsersLogic(IUserRepository UserRepo, IUnitOfWork Unit, IRepository<AppRole> RolesRepo):base(UserRepo,Unit)
        {
            this.RolesRepo = RolesRepo;
        }

        public AppRole GetRole(int id)
        {
            return RolesRepo.GetById(id);
        }

        public int CountUsers()
        {
            return Repo.CountUsersAsync();
        }
        
        public void ModifyUser(User user)
        {
            Unit.Context.Entry(user).State = System.Data.Entity.EntityState.Modified;
            Unit.SaveChanges();
        }
    }
}
