using DataModel.Identity;
using System.Threading.Tasks;

namespace Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        int CountUsersAsync();

        //IEnumerable<User> GetAllWithRoles();
    }
}
