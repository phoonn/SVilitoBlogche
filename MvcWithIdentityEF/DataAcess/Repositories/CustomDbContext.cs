using System.Data.Entity;

namespace DataAcess.Repositories
{
    public class CustomDbContext : DbContext
    {
        public CustomDbContext() : base("DbConnectionString")
        {
            //this.Users=this.Set<User>();
        }
    }
}
