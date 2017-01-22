using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Collections;
using System.Security.Principal;

namespace WebApplicationLogin.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim("FullName", this.Name));
            userIdentity.AddClaim(new Claim("Surname", this.Surname));
            userIdentity.AddClaim(new Claim("Email", this.Email));
            return userIdentity;
        }
        [Display(Name = "Imię ")]
        public string Name { get; set; }
        [Display(Name = "Nazwisko ")]
        public string Surname { get; set; }
        //[Display(Name = "Rola: ")]
        //public string RoleId { get; set; }
        //public virtual IdentityRole Role { get; set; }
        //[Display(Name ="Telefon: ")]
        //public string Telephone { get; set; }

        public virtual ICollection<News> Newses { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<MySubject> MySubjects { get; set; }
        public virtual SClass SClass { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<GradeList> GradeLists { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<MySubject> MySubjects { get; set; }
        public DbSet<SClass> SClasses { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<News> Newses { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Quiz> Quizs { get; set; }

        //visualowa podpowiedz
        public IEnumerable ApplicationUsers { get; internal set; }
    }

    //Klasa dodana
    public class IdentityManager
    {
        public RoleManager<IdentityRole> LocalRoleManager
        {
            get
            {
                return new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            }
        }


        public UserManager<ApplicationUser> LocalUserManager
        {
            get
            {
                return new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            }
        }


        public ApplicationUser GetUserByID(string userID)
        {
            ApplicationUser user = null;
            UserManager<ApplicationUser> um = this.LocalUserManager;

            user = um.FindById(userID);

            return user;
        }


        public ApplicationUser GetUserByName(string email)
        {
            ApplicationUser user = null;
            UserManager<ApplicationUser> um = this.LocalUserManager;

            user = um.FindByEmail(email);

            return user;
        }


        public bool RoleExists(string name)
        {
            var rm = LocalRoleManager;

            return rm.RoleExists(name);
        }


        public bool CreateRole(string name)
        {
            var rm = LocalRoleManager;
            var idResult = rm.Create(new IdentityRole(name));

            return idResult.Succeeded;
        }

        public bool DeleteRole(string name)
        {
            var rm = LocalRoleManager;
            var user = rm.FindByName(name);
            var idResult = rm.Delete(user);

            return idResult.Succeeded;
        }


        public bool CreateUser(ApplicationUser user, string password)
        {
            var um = LocalUserManager;
            var idResult = um.Create(user, password);

            return idResult.Succeeded;
        }


        public bool AddUserToRole(string userId, string roleName)
        {
            var um = LocalUserManager;
            var idResult = um.AddToRole(userId, roleName);

            return idResult.Succeeded;
        }


        public bool AddUserToRoleByUsername(string username, string roleName)
        {
            var um = LocalUserManager;

            string userID = um.FindByName(username).Id;
            var idResult = um.AddToRole(userID, roleName);

            return idResult.Succeeded;
        }


        public void ClearUserRoles(string userId)
        {
            var um = LocalUserManager;
            var user = um.FindById(userId);
            var currentRoles = new List<IdentityUserRole>();

            currentRoles.AddRange(user.Roles);

            foreach (var role in currentRoles)
            {
                um.RemoveFromRole(userId, role.RoleId);
            }
        }

    }

    public static class GenericPrincipalExtensions

    {
        public static string GetMySurname(this IPrincipal user)
        {
            if (user.Identity.IsAuthenticated)
            {
                ClaimsIdentity claimsIdentity = user.Identity as ClaimsIdentity;
                foreach (var claim in claimsIdentity.Claims)
                {
                    if (claim.Type == "Surname")
                        return claim.Value;
                }
                return "XYZ";
            }
            else
                return "XYZ1";
        }

        public static string GetMyFullName(this IPrincipal user)
        {
            if (user.Identity.IsAuthenticated)
            {
                ClaimsIdentity claimsIdentity = user.Identity as ClaimsIdentity;
                foreach (var claim in claimsIdentity.Claims)
                {
                    if (claim.Type == "FullName")
                        return claim.Value;
                }
                return "XYZ";
            }
            else
                return "XYZ1";
        }

        public static string GetMyEmail(this IPrincipal user)
        {
            if (user.Identity.IsAuthenticated)
            {
                ClaimsIdentity claimsIdentity = user.Identity as ClaimsIdentity;
                foreach (var claim in claimsIdentity.Claims)
                {
                    if (claim.Type == "Email")
                        return claim.Value;
                }
                return "XYZ";
            }
            else
                return "XYZ1";

        }
    }
}