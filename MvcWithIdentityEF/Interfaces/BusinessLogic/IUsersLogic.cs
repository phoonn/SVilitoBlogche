using DataModel.Identity;
using System.Threading.Tasks;

namespace Interfaces.BusinessLogic
{
    public interface IUsersLogic : ICrudLogic<User>
    {
        AppRole GetRole(int id);

        int CountUsers();

        void ModifyUser(User user);
    }
}
