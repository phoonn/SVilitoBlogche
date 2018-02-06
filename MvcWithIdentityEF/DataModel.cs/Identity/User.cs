using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;

namespace DataModel.Identity
{
    public class User : IdentityUser<int,AppUserLogin,AppUserRole,AppUserClaim>
    {
        public override int Id { get; set; }
    }
    public class AppRole : IdentityRole<int, AppUserRole>
    {
        public AppRole() { }
        public AppRole(string name)
        {
            Name = name;
        }
    }
    public class AppUserRole : IdentityUserRole<int> { }
    public class AppUserClaim : IdentityUserClaim<int> { }
    public class AppUserLogin : IdentityUserLogin<int> { }

    public class AppClaimsPrincipal : ClaimsPrincipal
    {
        public AppClaimsPrincipal(ClaimsPrincipal principal) : base(principal)
        { }

        public int UserId
        {
            get { return int.Parse(this.FindFirst(ClaimTypes.Sid).Value); }
        }
    }
}
