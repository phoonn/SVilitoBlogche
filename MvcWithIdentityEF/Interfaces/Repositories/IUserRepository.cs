using DataModel.Models.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<int> CountUsersAsync();

        //IEnumerable<User> GetAllWithRoles();
    }
}
