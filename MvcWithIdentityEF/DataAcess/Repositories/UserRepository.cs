using DataModel.Models.Identity;
using Interfaces.Repositories;
using System.Data.Entity;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace DataAcess.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IUnitOfWork unit) : base(unit)
        {
        }

        public Task<int> CountUsersAsync()
        {
            return Items.CountAsync();
        }

        //public IEnumerable<User> GetAllWithRoles()
        //{
        //    return context.
        //}
    }
}
