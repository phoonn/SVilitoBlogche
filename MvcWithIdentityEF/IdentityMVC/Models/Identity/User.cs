using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace IdentityMVC.Models.Identity
{
    public class User : IdentityUser<int,AppUserLogin,AppUserRole,AppUserClaim>
    {
        public override int Id { get; set; }

        //[Column(TypeName ="varchar"), StringLength(64)]
        //public override string UserName { get; set; }
        //[Column(TypeName = "varchar"), StringLength(128)]
        //public override string Email { get; set; }
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
