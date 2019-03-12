using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Configuration;

namespace DataModel.Identity.ManagerAndStore
{
    public class MyIdentityAppContext : IdentityDbContext<User,AppRole,int,AppUserLogin,AppUserRole,AppUserClaim>
    {
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<PostComment> PostComment { get; set; }

        public MyIdentityAppContext () : base("DbConnectionString")
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<MyIdentityAppContext, Migration.Configuration>(true));

            Database.SetInitializer<MyIdentityAppContext>(null);

            this.BlogPosts = this.Set<BlogPost>();
            this.PostComment = this.Set<PostComment>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<User>().ToTable("MyUsers").Property(p => p.Id).HasColumnName("UserId");
            modelBuilder.Entity<AppUserRole>().ToTable("UserRoles").HasKey(i=>new { i.RoleId, i.UserId });
            modelBuilder.Entity<AppUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<AppUserClaim>().ToTable("UserClaims").HasKey(i => i.Id);
            modelBuilder.Entity<AppRole>().ToTable("Roles").HasKey(i=>i.Id);

            modelBuilder.Entity<User>().Property(e => e.UserName).HasColumnType("VARCHAR").HasMaxLength(64);
            modelBuilder.Entity<User>().Property(e => e.Email).HasColumnType("VARCHAR").HasMaxLength(128);
            modelBuilder.Entity<AppRole>().Property(e=>e.Name).HasColumnType("VARCHAR").HasMaxLength(64);

            modelBuilder.Entity<PostComment>()
                .HasRequired(c => c.BlogPost)
                .WithMany()
                .WillCascadeOnDelete(true);

            //modelBuilder.Entity<Side>()
            //    .HasRequired(s => s.Stage)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);
        }
    }
}