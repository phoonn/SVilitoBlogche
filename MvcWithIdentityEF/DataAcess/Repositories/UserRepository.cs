using Interfaces.Repositories;
using System.Data.Entity;
using System.Threading.Tasks;
using DataModel.Identity;
using System;

namespace DataAcess.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IUnitOfWork unit) : base(unit)
        {
        }

        public int CountUsersAsync()
        {
            Task<int> t = Items.CountAsync();
            return t.Result;
            
        }

        //public IEnumerable<User> GetAllWithRoles()
        //{
        //    return context.
        //}
    }
}
